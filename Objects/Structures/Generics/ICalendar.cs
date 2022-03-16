namespace Mercury.Snapshot.Objects.Structures.Generics
{
    public interface ICalendar
    {
        public Task<IReadOnlyCollection<IEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults);
    }
}
