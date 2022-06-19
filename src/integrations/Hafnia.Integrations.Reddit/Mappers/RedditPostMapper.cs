using Hafnia.Integrations.Reddit.Models;
using Hafnia.Integrations.Shared.Helpers;
using Json = Hafnia.Integrations.Reddit.Models.Internal.Json;

namespace Hafnia.Integrations.Reddit.Mappers;

internal static class RedditPostMapper
{
    public static RedditPost MapToModel(Json.RedditLink link)
    {
        RedditPostMediaMetadata[] metaDatas = link.MediaMetadata.Select(kv => MapToModel(kv.Value)).ToArray();
        RedditPostGalleryItem[] galleryItems = link.GalleryData.Items.Select(i => MapToModel(i, metaDatas)).ToArray();

        return new RedditPost
            (
                Id: link.Id,
                Name: link.Name,

                IsGallery: link.IsGallery,
                IsVideo: link.IsVideo,

                GalleryItems: galleryItems,
                MediaMetadata: metaDatas
            );
    }

    private static RedditPostGalleryItem MapToModel(Json.GalleryDataItem galleryItem, RedditPostMediaMetadata[] metadataItems)
    {
        RedditPostMediaMetadata mediaMetadata = metadataItems.Single(m => m.MediaId == galleryItem.MediaId);

        return new RedditPostGalleryItem
        (
            Id: galleryItem.Id,
            MediaId: galleryItem.MediaId,
            Caption: galleryItem.Caption,
            Metadata: mediaMetadata
        );
    }

    private static RedditPostMediaMetadata MapToModel(Json.MediaMetadata metadata)
    {
        return new RedditPostMediaMetadata
        (
            MediaId: metadata.Id,
            MimeType: metadata.M,
            Extension: MimeHelper.ConvertToExtension(metadata.M),
            Previews: metadata.P.Select(MapToModel).ToArray(),
            Source: MapToModel(metadata.S)
        );
    }

    private static RedditPostMediaMetadataInstance MapToModel(Json.MediaMetadataInstance instance)
    {
        return new RedditPostMediaMetadataInstance
        (
            Height: instance.Y,
            Width: instance.X,
            Uri: new Uri(instance.U)
        );
    }
}
