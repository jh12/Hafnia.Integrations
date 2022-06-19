namespace Hafnia.Integrations.Reddit.Exceptions
{
    public class NotGalleryException : RedditException
    {
        public Uri Url { get; }

        public NotGalleryException(Uri url) : base($"No gallery could be located at {url}")
        {
            Url = url;
        }
    }
}
