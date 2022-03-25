using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars
{
    internal interface ICalendar
    {
        internal Task<IReadOnlyCollection<CalendarEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults);
        internal Task SaveEvents(params CalendarEvent[] Events);
    }
}
