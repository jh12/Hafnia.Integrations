namespace Hafnia.Integrations.Reddit.Exceptions
{
    public class NoSourceException : Exception
    {
        public Uri Url { get; }

        public NoSourceException(Uri url) : base($"No source(s) found for {url}")
        {
            Url = url;
        }

        public NoSourceException(Uri url, string message) : base(message)
        {
            Url = url;
        }
    }
}
