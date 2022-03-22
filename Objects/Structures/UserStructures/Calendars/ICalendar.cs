using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars
{
    public interface ICalendar
    {
        public Task<IReadOnlyCollection<IEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults);
        public Task SaveEvents(params IEvent[] Events);
    }
}
