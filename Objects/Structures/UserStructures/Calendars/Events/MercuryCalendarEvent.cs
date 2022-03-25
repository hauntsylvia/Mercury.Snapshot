using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events
{
    internal class MercuryCalendarEvent : CalendarEvent
    {
        internal MercuryCalendarEvent(string Summary, string Description, DateTime LastUpdated, DateTime Created, DateTime Start, DateTime End, Origins Origin, ulong Id) : base(Summary, Description, LastUpdated, Created, Start, End, Origin, Id)
        {
        }
    }
}
