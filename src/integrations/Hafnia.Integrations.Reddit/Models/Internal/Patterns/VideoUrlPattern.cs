using Hafnia.Integrations.Reddit.Clients;

namespace Hafnia.Integrations.Reddit.Models.Internal.Patterns;

internal record VideoUrlPattern : UrlPattern
{
    private readonly IRedditClient _client;

    public VideoUrlPattern(IRedditClient client, string regexPattern) : base(regexPattern)
    {
        _client = client;
    }

    public override Task<RedditMedia> GetMediaForUrlAsync(Uri url)
    {
        throw new NotImplementedException("Url pattern marked as unsupported");
    }
}
