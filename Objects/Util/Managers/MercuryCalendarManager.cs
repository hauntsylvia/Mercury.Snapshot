using Mercury.Snapshot.Objects.Structures.Calendars;
using Mercury.Snapshot.Objects.Structures.Personalization;

namespace Mercury.Snapshot.Objects.Util.Managers
{
    public class MercuryCalendarManager
    {
        public MercuryCalendarManager(MercuryProfile Profile, List<ICalendar> AllCalendarsToSync)
        {
            this.Profile = Profile;
            this.AllCalendarsToSync = AllCalendarsToSync;
        }

        public MercuryProfile Profile { get; }
        public IReadOnlyCollection<ICalendar> AllCalendarsToSync { get; }

        public Task SyncAllCalendars()
        {
            return Task.CompletedTask;
        }
    }
}
