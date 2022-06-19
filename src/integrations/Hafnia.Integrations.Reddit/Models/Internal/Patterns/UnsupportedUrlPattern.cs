namespace Hafnia.Integrations.Reddit.Models.Internal.Patterns;

internal record UnsupportedUrlPattern : UrlPattern
{
    public UnsupportedUrlPattern(string regexPattern) : base(regexPattern)
    {
    }

    public override Task<RedditMedia> GetMediaForUrlAsync(Uri url)
    {
        throw new NotImplementedException("Url pattern marked as unsupported");
    }
}
