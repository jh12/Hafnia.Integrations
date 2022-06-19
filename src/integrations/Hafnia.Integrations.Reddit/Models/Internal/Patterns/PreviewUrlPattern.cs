using Hafnia.Integrations.Reddit.Clients;

namespace Hafnia.Integrations.Reddit.Models.Internal.Patterns;

internal record PreviewUrlPattern : UrlPattern
{
    private readonly IRedditClient _client;

    public PreviewUrlPattern(IRedditClient client, string regexPattern) : base(regexPattern)
    {
        _client = client;
    }

    public override async Task<RedditMedia> GetMediaForUrlAsync(Uri url)
    {
        Uri downloadUrl = await TryGetDownloadUrlAsync(url);

        RedditPreview preview = new RedditPreview(url, downloadUrl);

        return preview;
    }

    private async Task<Uri> TryGetDownloadUrlAsync(Uri previewUrl)
    {
        UrlPatternParts parts = GetPatternParts(previewUrl);

        Uri imageUrl = new Uri($"https://i.redd.it/{parts.Id}.{parts.Extension}");

        if (await _client.CheckIfUrlExistsAsync(imageUrl))
            return imageUrl;

        throw new NotImplementedException("No other download url image patterns defined");
    }
}
