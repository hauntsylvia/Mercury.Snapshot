namespace Mercury.Snapshot.Objects.Structures.Exceptions
{
    internal class MercuryColdStartupException : Exception
    {
        internal MercuryColdStartupException(string? Message) : base(Message)
        {
        }

        internal MercuryColdStartupException()
        {
        }

        internal MercuryColdStartupException(string Message, Exception InnerException) : base(Message, InnerException)
        {
        }
    }
}
