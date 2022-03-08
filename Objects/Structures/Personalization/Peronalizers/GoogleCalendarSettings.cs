using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers
{
    internal class GoogleCalendarSettings
    {
        internal GoogleCalendarSettings(string CalendarId = "primary")
        {
            this.CalendarId = CalendarId;
        }

        public string CalendarId { get; }
    }
}
