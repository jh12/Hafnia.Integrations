namespace Hafnia.Integrations.Reddit.Models;

public record RedditImage
(
    string Id,
    string? Name,
    Uri Url,
    Uri DownloadUrl,
    string? Extension
)
: RedditMedia
(
    Url,
    DownloadUrl
);
