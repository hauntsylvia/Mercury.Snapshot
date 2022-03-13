using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Snapshot.Objects.Util.Generics;
using Mercury.Snapshot.Objects.Util.Google.Calendar;
using Mercury.Unification.IO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
