using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class CalendarEventLoggedEmbed : EmbedBuilder
    {
        public CalendarEventLoggedEmbed(CalendarEvent Event, CultureInfo Culture)
        {
            this.Color = EmbedDefaults.EmbedColor;
            this.Description = $"{Event.Start.ToString(Culture.DateTimeFormat.LongDatePattern, Culture)} at {Event.Start.ToString(Culture.DateTimeFormat.ShortTimePattern, Culture)}\n```{Event.Description}```";
            this.Title = Event.Summary;
            this.Timestamp = Event.Start;
        }
    }
}
