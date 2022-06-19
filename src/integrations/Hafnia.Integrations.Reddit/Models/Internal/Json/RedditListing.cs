namespace Hafnia.Integrations.Reddit.Models.Internal.Json;

internal record RedditListing<T>
{
    public string? Before { get; init; }
    public string? After { get; init; }
    public string Modhash { get; init; } = null!;
    public T[] Children { get; init; } = Array.Empty<T>();
}
