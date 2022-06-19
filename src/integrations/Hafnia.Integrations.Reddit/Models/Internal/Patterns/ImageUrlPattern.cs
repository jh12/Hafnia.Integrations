using Hafnia.Integrations.Reddit.Clients;

namespace Hafnia.Integrations.Reddit.Models.Internal.Patterns;

internal record ImageUrlPattern : UrlPattern
{
    private readonly IRedditClient _client;

    public ImageUrlPattern(IRedditClient client, string regexPattern) : base(regexPattern)
    {
        _client = client;
    }

    public override Task<RedditMedia> GetMediaForUrlAsync(Uri url)
    {
        UrlPatternParts parts = GetPatternParts(url);

        RedditMedia image = new RedditImage(parts.Id, null, url, url, parts.Extension);

        return Task.FromResult(image);
    }
}
