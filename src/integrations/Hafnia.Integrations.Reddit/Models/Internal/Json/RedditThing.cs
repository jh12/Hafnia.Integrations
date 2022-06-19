namespace Hafnia.Integrations.Reddit.Models.Internal.Json;

public record RedditThing<T>
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string Kind { get; init; }
    public T Data { get; init; }
};
