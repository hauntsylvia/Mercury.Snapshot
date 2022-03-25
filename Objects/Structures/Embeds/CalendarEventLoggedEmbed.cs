using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    internal class CalendarEventLoggedEmbed : EmbedBuilder
    {
        internal CalendarEventLoggedEmbed(CalendarEvent Event)
        {
            this.Description = $"{Event.Start.ToLongDateString()} at {Event.Start.ToShortTimeString()}\n```{Event.Description}```";
            this.Title = Event.Summary;
            this.Timestamp = Event.Start;
        }
    }
}
