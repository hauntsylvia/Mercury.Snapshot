using Mercury.Snapshot.Objects.Structures.Generics;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Unification.IO.File;

namespace Mercury.Snapshot.Objects.Util.Mercury.Calendar
{
    public class MercuryCalendarManager
    {
        public MercuryCalendarManager(MercuryProfile Profile, List<ICalendar> AllCalendarsToSync)
        {
            this.Profile = Profile;
            this.CalendarRegister = new("Mercury Calendars");
            this.AllCalendarsToSync = AllCalendarsToSync;
        }

        public MercuryProfile Profile { get; }
        public Register CalendarRegister { get; }
        public IReadOnlyCollection<ICalendar> AllCalendarsToSync { get; }

        public Task SyncAllCalendars()
        {
            return Task.CompletedTask;
        }
    }
}
