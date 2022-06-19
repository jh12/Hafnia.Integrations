using Hafnia.Integrations.Reddit.Models;

namespace Hafnia.Integrations.Reddit.Clients;

internal interface IRedditClient
{
    Task<bool> CheckIfUrlExistsAsync(Uri url);

    Task<RedditPost> GetPostAsync(string postId);
    Task<RedditPost> GetPostAsync(Uri url);

    Uri GetImageDownloadUri(string id, string extension);
    Uri GetPostUri(string id, bool asJson = true);
}
