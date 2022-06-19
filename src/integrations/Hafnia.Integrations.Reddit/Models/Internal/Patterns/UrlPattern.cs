using System.Text.RegularExpressions;

namespace Hafnia.Integrations.Reddit.Models.Internal.Patterns;

internal record UrlPattern
{
    private readonly Regex _regex;

    public UrlPattern(string regexPattern)
    {
        _regex = new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }

    public bool HasMatch(Uri url)
    {
        return _regex.IsMatch(url.AbsoluteUri);
    }

    public UrlPatternParts GetPatternParts(Uri url)
    {
        Match match = _regex.Match(url.AbsoluteUri);

        if (!match.Success)
            throw new ArgumentException("Pattern did not match parameter", nameof(url));

        string id = match.Groups["id"].Value;

        UrlPatternParts parts = new UrlPatternParts(id, null, null);

        if (match.Groups.TryGetValue("ext", out Group? extGroup))
            parts = parts with { Extension = extGroup.Value };

        if (match.Groups.TryGetValue("post_id", out Group? postIdGroup))
            parts = parts with { PostId = postIdGroup.Value };

        return parts;
    }

    public virtual Task<RedditMedia> GetMediaForUrlAsync(Uri url)
    {
        throw new NotImplementedException();
    }
}
