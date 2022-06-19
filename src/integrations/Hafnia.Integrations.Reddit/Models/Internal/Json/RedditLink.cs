using System.Text.Json.Serialization;

namespace Hafnia.Integrations.Reddit.Models.Internal.Json;

internal record RedditLink
{
    #region Thing

    public string Id { get; init; }
    public string Name { get; init; }

    #endregion

    #region Votable

    public int Ups { get; init; }
    public int Downs { get; init; }

    #endregion

    #region Created

    [JsonPropertyName("created")]
    public decimal Created { get; init; }
    [JsonPropertyName("created_utc")]
    public decimal CreatedUtc { get; init; }

    #endregion

    #region Link

    [JsonPropertyName("author")]
    public string Author { get; init; }
    [JsonPropertyName("author_flair_css_class")]
    public string AuthorFlairCssClass { get; init; }
    [JsonPropertyName("author_flair_text")]
    public string AuthorFlairText { get; init; }
    // Clicked
    public string Domain { get; init; }
    public bool Hidden { get; init; }
    [JsonPropertyName("is_self")]
    public bool IsSelf { get; init; }
    public bool? Likes { get; init; }
    [JsonPropertyName("link_flair_css_class")]
    public string LinkFlairCssClass { get; init; }
    [JsonPropertyName("link_flair_text")]
    public string LinkFlairText { get; init; }
    public bool Locked { get; init; }
    // Media
    // Media_embed
    [JsonPropertyName("num_comments")]
    public int NumComments { get; init; }
    [JsonPropertyName("over_18")]
    public bool Over18 { get; init; }
    [JsonPropertyName("perma_link")]
    public string Permalink { get; init; }
    public bool Saved { get; init; }
    public int Score { get; init; }
    [JsonPropertyName("self_text")]
    public string SelfText { get; init; }
    [JsonPropertyName("self_text_html")]
    public string SelfTextHtml { get; init; }
    public string Subreddit { get; init; }
    [JsonPropertyName("subreddit_id")]
    public string SubredditId { get; init; }
    public string Thumbnail { get; init; }
    public string Title { get; init; }
    public string Url { get; init; }
    public bool Edited { get; init; }
    public string Distinguished { get; init; }
    public bool Stickied { get; init; }

    [JsonPropertyName("is_gallery")]
    public bool IsGallery { get; init; }
    [JsonPropertyName("is_original_content")]
    public bool IsOriginalContent { get; init; }
    [JsonPropertyName("is_reddit_media_domain")]
    public bool IsRedditMediaDomain { get; init; }
    [JsonPropertyName("is_meta")]
    public bool IsMeta { get; init; }
    [JsonPropertyName("is_video")]
    public bool IsVideo { get; init; }

    // Media
    [JsonPropertyName("media_metadata")]
    public Dictionary<string, MediaMetadata> MediaMetadata { get; init; }
    [JsonPropertyName("gallery_data")]
    public GalleryData GalleryData { get; init; }

    #endregion
}

internal record MediaMetadata
{
    public string Status { get; init; }
    public string E { get; init; }
    public string M { get; init; }
    public MediaMetadataInstance[] P { get; init; } = Array.Empty<MediaMetadataInstance>();
    public MediaMetadataInstance S { get; init; }
    public string Id { get; init; }
}

internal record MediaMetadataInstance(short Y, short X, string U);

internal record GalleryData
{
    public GalleryDataItem[] Items { get; init; } = Array.Empty<GalleryDataItem>();
}

internal record GalleryDataItem
{
    [JsonPropertyName("media_id")]
    public string MediaId { get; init; }
    public long Id { get; init; }
    public string? Caption { get; set; }

};
