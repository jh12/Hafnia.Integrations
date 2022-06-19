namespace Hafnia.Integrations.Reddit.Models;

public record RedditPreview
(
    Uri Url,
    Uri? DownloadUrl
)
: RedditMedia
(
    Url,
    DownloadUrl
);
