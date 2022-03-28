namespace Mercury.Snapshot.Objects.Structures.Exceptions
{
    public class MercuryColdStartupException : Exception
    {
        public MercuryColdStartupException(string? Message) : base(Message)
        {
        }

        public MercuryColdStartupException()
        {
        }

        public MercuryColdStartupException(string Message, Exception InnerException) : base(Message, InnerException)
        {
        }
    }
}
