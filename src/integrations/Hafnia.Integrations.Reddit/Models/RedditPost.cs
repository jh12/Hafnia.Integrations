namespace Hafnia.Integrations.Reddit.Models;

public record RedditPost
(
    // General
    string Id,
    string Name,

    // Media

    // Indicators
    bool IsGallery,
    bool IsVideo,

    RedditPostGalleryItem[] GalleryItems,
    RedditPostMediaMetadata[] MediaMetadata
);

public record RedditPostGalleryItem(long Id, string MediaId, string? Caption, RedditPostMediaMetadata Metadata);
public record RedditPostMediaMetadata(string MediaId, string MimeType, string Extension, RedditPostMediaMetadataInstance[] Previews, RedditPostMediaMetadataInstance Source);

public record RedditPostMediaMetadataInstance(int Height, int Width, Uri Uri);

