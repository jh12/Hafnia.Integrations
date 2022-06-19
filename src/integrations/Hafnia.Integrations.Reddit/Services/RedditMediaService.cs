using Hafnia.Integrations.Reddit.Clients;
using Hafnia.Integrations.Reddit.Models;
using Hafnia.Integrations.Reddit.Models.Internal.Patterns;
using Hafnia.Integrations.Shared.Services;

namespace Hafnia.Integrations.Reddit.Services;

internal class RedditMediaService : IMediaService
{
    private readonly IRedditClient _redditClient;

    private UrlPattern[] _patterns;

    public RedditMediaService(IRedditClient redditClient)
    {
        _redditClient = redditClient;

        _patterns = new UrlPattern[]
        {
            new ImageUrlPattern(redditClient, @"https?:\/\/i\.redd\.it\/(?<id>[\w\d]{10,20})\.(?<ext>\w{2,6})"),
            new GalleryUrlPattern(redditClient, @"https?:\/\/www\.reddit\.com\/gallery\/(?<post_id>[\w\d]{6,10})"),
            new VideoUrlPattern(redditClient, @"https?:\/\/v\.redd\.it\/(?<id>[\w\d]{10,20})"),
            new PreviewUrlPattern(redditClient, @"https?:\/\/preview\.redd\.it\/(?<id>[\w\d]{10,20})\.(?<ext>\w{2,6})?(.+)"),
            new UnsupportedUrlPattern(@"https?:\/\/b\.thumbs\.redditmedia\.com\/.+")
        };
    }

    public async Task<RedditMedia?> GetMediaForUrlAsync(Uri url)
    {
        UrlPattern? matchingPattern = _patterns.SingleOrDefault(p => p.HasMatch(url));

        if (matchingPattern == null)
            return null;

        RedditMedia media = await matchingPattern.GetMediaForUrlAsync(url);

        if (media is RedditPreview preview && preview.DownloadUrl != null)
        {
            return await GetMediaForUrlAsync(preview.DownloadUrl);
        }

        return media;
    }
}
