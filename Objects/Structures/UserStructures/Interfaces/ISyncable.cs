namespace Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces
{
    public interface ISyncable
    {
        public Task Pull();
        public Task Push();
    }
}
