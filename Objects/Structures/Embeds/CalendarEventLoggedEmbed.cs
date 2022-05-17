using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using System.Globalization;

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
