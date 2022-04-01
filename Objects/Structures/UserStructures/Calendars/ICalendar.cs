using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars
{
    public interface ICalendar
    {
        public Task<IReadOnlyCollection<CalendarEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults);
        public Task SaveEvents(params CalendarEvent[] Events);
        public Task DeleteEvents(params CalendarEvent[] Events);
    }
}
