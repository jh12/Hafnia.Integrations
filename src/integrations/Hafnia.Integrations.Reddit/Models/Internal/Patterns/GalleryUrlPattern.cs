using Hafnia.Integrations.Reddit.Clients;
using Hafnia.Integrations.Reddit.Exceptions;

namespace Hafnia.Integrations.Reddit.Models.Internal.Patterns;

internal record GalleryUrlPattern : UrlPattern
{
    private readonly IRedditClient _client;

    public GalleryUrlPattern(IRedditClient client, string regexPattern) : base(regexPattern)
    {
        _client = client;
    }

    public override async Task<RedditMedia> GetMediaForUrlAsync(Uri url)
    {
        UrlPatternParts parts = GetPatternParts(url);

        RedditPost galleryPost = await _client.GetPostAsync(parts.PostId!);

        if (!galleryPost.IsGallery)
            throw new NotGalleryException(url);

        RedditGalleryItem[] galleryItems = galleryPost.GalleryItems.Select(g =>
        {
            var metadata = g.Metadata;

            if (metadata.Source == null)
                throw new NoSourceException(url, "No source found for gallery item");

            RedditPostMediaMetadataInstance source = metadata.Source!;
            Uri downloadUri = _client.GetImageDownloadUri(metadata.MediaId, metadata.Extension);

            return new RedditGalleryItem(g.Caption, g.MediaId, metadata.MimeType, metadata.Extension, downloadUri, source.Height, source.Width);
        }).ToArray();

        return new RedditGallery(galleryItems, url);
    }
}
