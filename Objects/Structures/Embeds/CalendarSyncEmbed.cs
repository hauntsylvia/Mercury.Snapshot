using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class CalendarSyncEmbed : EmbedBuilder
    {
        public CalendarSyncEmbed(bool Loading = true, bool Success = true)
        {
            this.Color = EmbedDefaults.EmbedColor;
            this.Timestamp = DateTime.UtcNow;
            this.Description = Loading
                ? $"{Strings.EmbedStrings.Calendars.CalendarSyncLoading} {Emotes.AliceCenturion}"
                : Success ? $"{Strings.EmbedStrings.Calendars.CalendarSyncSuccess}" : $"{Strings.EmbedStrings.Calendars.CalendarSyncFailure}";
        }
    }
}
