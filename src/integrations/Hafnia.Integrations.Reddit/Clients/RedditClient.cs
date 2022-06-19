using System.Net.Http.Json;
using System.Text.Json;
using Hafnia.Integrations.Reddit.Mappers;
using Hafnia.Integrations.Reddit.Models;
using Json = Hafnia.Integrations.Reddit.Models.Internal.Json;

namespace Hafnia.Integrations.Reddit.Clients;

internal class RedditClient : IRedditClient
{
    private readonly HttpClient _httpClient;
    private JsonSerializerOptions _jsonSerializerOptions;

    public RedditClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<bool> CheckIfUrlExistsAsync(Uri url)
    {
        using (HttpResponseMessage response = await _httpClient.GetAsync(url))
        {
            return response.IsSuccessStatusCode;
        }
    }

    public Task<RedditPost> GetPostAsync(string postId)
    {
        Uri postUri = GetPostUri(postId);
        return GetPostAsync(postUri);
    }

    public async Task<RedditPost> GetPostAsync(Uri url)
    {
        var redditResponse = await _httpClient.GetFromJsonAsync<Json.RedditThing<Json.RedditListing<Json.RedditThing<Json.RedditLink>>>[]>(url, _jsonSerializerOptions);

        Json.RedditLink redditLink = redditResponse!
            .SelectMany(r => r.Data.Children)
            .Single(c => c.Kind == "t3")
            .Data;

        return RedditPostMapper.MapToModel(redditLink);
    }

    public Uri GetPostUri(string id, bool asJson = true)
    {
        if (asJson)
            return new Uri($"https://www.reddit.com/comments/{id}.json");

        return new Uri($"https://www.reddit.com/comments/{id}");
    }

    public Uri GetImageDownloadUri(string id, string extension)
    {
        return new Uri($"https://i.redd.it/{id}.{extension}");
    }
}
