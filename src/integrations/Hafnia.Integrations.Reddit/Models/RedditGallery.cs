namespace Hafnia.Integrations.Reddit.Models;

public record RedditGallery
(
    RedditGalleryItem[] Items,
    Uri Url
)
: RedditMedia
(
    Url,
    null
);

public record RedditGalleryItem
(
    string? Caption, 
    string MediaId, 
    string MimeType, 
    string Extension, 
    Uri DownloadUri,
    int Height,
    int Width
);
