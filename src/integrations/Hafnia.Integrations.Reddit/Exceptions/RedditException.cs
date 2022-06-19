using System.Runtime.Serialization;

namespace Hafnia.Integrations.Reddit.Exceptions
{
    public class RedditException : Exception
    {
        public RedditException()
        {
        }

        protected RedditException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RedditException(string? message) : base(message)
        {
        }

        public RedditException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
