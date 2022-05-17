namespace Mercury.Snapshot.Objects.Structures.UserStructures.Identification
{
    public static class Identifier
    {
        private static ulong lastContender = (ulong)(DateTime.UtcNow - DateTime.UnixEpoch).TotalMilliseconds;
        public static ulong GetIdentifier()
        {
            lastContender = Interlocked.Increment(ref lastContender);
            return lastContender;
        }
    }
}
