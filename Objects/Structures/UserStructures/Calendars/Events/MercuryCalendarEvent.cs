using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events
{
    public class MercuryCalendarEvent : CalendarEvent
    {
        public MercuryCalendarEvent(string Summary, string Description, DateTime LastUpdated, DateTime Created, DateTime Start, DateTime End, Origins Origin, ulong Id) : base(Summary, Description, LastUpdated, Created, Start, End, Origin, Id)
        {
        }
    }
}
