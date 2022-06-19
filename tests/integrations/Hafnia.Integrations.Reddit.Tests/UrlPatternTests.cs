using System.Net;
using FluentAssertions;
using Hafnia.Integrations.Reddit.Models;
using Hafnia.Medias.Tests.Shared.Helpers;
using Moq;
using Moq.Protected;

namespace Hafnia.Integrations.Reddit.Tests;

internal class UrlPatternTests : RedditTestBase
{
    [TestCase("https://i.redd.it/abcd1234abcd1.jpg", "https://i.redd.it/abcd1234abcd1.jpg", "abcd1234abcd1", "jpg")]
    [TestCase("https://i.redd.it/aaaaaaaa1bb22.png", "https://i.redd.it/aaaaaaaa1bb22.png", "aaaaaaaa1bb22", "png")]
    public async Task ShouldReturnRedditImage(Uri uri, Uri downloadUri, string id, string extension)
    {
        RedditMedia? media = await MediaService.GetMediaForUrlAsync(uri);

        media.Should().NotBeNull();
        media.Should().BeOfType<RedditImage>();

        RedditImage image = (RedditImage)media!;

        image.Id.Should().Be(id);
        image.Extension.Should().Be(extension);
        image.Url.Should().Be(uri);
        image.DownloadUrl.Should().Be(downloadUri);
    }

    [TestCase("https://www.reddit.com/gallery/abcdef", "https://www.reddit.com/comments/abcdef.json")]
    public async Task ShouldReturnRedditGallery(Uri uri, Uri postUri)
    {
        HttpHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                )
            .ReturnsAsync((HttpRequestMessage req, CancellationToken _) => new HttpResponseMessage
            {
                StatusCode = req.RequestUri == postUri ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                Content = req.RequestUri == postUri ? new StringContent(DataFileHelper.GetText("Data/RedditGalleryPost.json")) : null
            }).Verifiable();

        RedditMedia? media = await MediaService.GetMediaForUrlAsync(uri);

        media.Should().NotBeNull();
        media.Should().BeOfType<RedditGallery>();

        RedditGallery gallery = (RedditGallery)media!;

        gallery.Items.Should().HaveCount(6);

        RedditGalleryItem galleryItem = gallery.Items[3];
        galleryItem.Caption.Should().Be("Testable image");
        galleryItem.MediaId.Should().Be("mkonjibhuvgyc");
        galleryItem.MimeType.Should().Be("image/jpg");
        galleryItem.Extension.Should().Be("jpg");
        galleryItem.DownloadUri.Should().Be(new Uri("https://i.redd.it/mkonjibhuvgyc.jpg"));
        galleryItem.Height.Should().Be(1080);
        galleryItem.Width.Should().Be(1920);
    }

    [TestCase("https://preview.redd.it/abcd1234abcd1.jpg?auto=webp&s=123456789123456789abcdefghijklmnopqrstuv", "https://i.redd.it/abcd1234abcd1.jpg", "abcd1234abcd1", "jpg")]
    [TestCase("https://preview.redd.it/aaaaaaaa1bb22.png?auto=webp&amp;s=123456789123456789abcdefghijklmnopqrstuv", "https://i.redd.it/aaaaaaaa1bb22.png", "aaaaaaaa1bb22", "png")]
    [TestCase("https://preview.redd.it/aaaaaaaa1bb22.png?width=108&amp;crop=smart&amp;auto=webp&amp;s=abcdefghijklmnopqrstuv123456789123456789", "https://i.redd.it/aaaaaaaa1bb22.png", "aaaaaaaa1bb22", "png")]
    [TestCase("https://preview.redd.it/aaaaaaaa1bb22.png?width=108&crop=smart&auto=webp&s=abcdefghijklmnopqrstuv123456789123456789", "https://i.redd.it/aaaaaaaa1bb22.png", "aaaaaaaa1bb22", "png")]
    public async Task ShouldReturnRedditImagePreview(Uri uri, Uri downloadUri, string id, string extension)
    {
        HttpHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync((HttpRequestMessage req, CancellationToken _) => new HttpResponseMessage()
            {
                StatusCode = req.RequestUri == downloadUri ? HttpStatusCode.OK : HttpStatusCode.NotFound
            }).Verifiable();

        RedditMedia? media = await MediaService.GetMediaForUrlAsync(uri);

        media.Should().NotBeNull();
        media.Should().BeOfType<RedditImage>();

        RedditImage image = (RedditImage)media!;

        image.Id.Should().Be(id);
        image.Extension.Should().Be(extension);
        image.Url.Should().Be(downloadUri);
        image.DownloadUrl.Should().Be(downloadUri);
    }

    [TestCase("https://b.thumbs.redditmedia.com/123456789123456789abcdefghijklmnopqrstuv.jpg")]
    [TestCase("https://v.redd.it/abcdefghi1234")]
    public async Task ShouldThrowExceptionNotImplemented(Uri uri)
    {
        await MediaService.Invoking(s => s.GetMediaForUrlAsync(uri))
            .Should().ThrowAsync<NotImplementedException>()
            .WithMessage("Url pattern marked as unsupported");
    }
}
