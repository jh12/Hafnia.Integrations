using Hafnia.Integrations.Reddit.Clients;
using Hafnia.Integrations.Reddit.Services;
using Moq;

namespace Hafnia.Integrations.Reddit.Tests;

internal class RedditTestBase
{
    protected Mock<HttpMessageHandler> HttpHandlerMock;
    protected IRedditClient RedditClient;
    protected RedditMediaService MediaService;

    [SetUp]
    public void Setup()
    {
        HttpHandlerMock = new Mock<HttpMessageHandler>();

        HttpClient httpClient = new HttpClient(HttpHandlerMock.Object);

        RedditClient = new RedditClient(httpClient);
        MediaService = new RedditMediaService(RedditClient);
    }
}
