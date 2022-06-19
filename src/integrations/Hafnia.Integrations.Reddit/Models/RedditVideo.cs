namespace Hafnia.Integrations.Reddit.Models;

public record RedditVideo
(
    string Id,
    string? Name,
    Uri Url,
    Uri? DownloadUrl,
    string? Extension
)
: RedditMedia
(
    Url,
    DownloadUrl
);
