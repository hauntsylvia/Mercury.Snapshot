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
        public CalendarSyncEmbed(bool Loading = true)
        {
            this.Timestamp = DateTime.UtcNow;
            if (Loading)
            {
                this.Description = $"{Strings.EmbedStrings.Calendars.CalendarSyncLoading} {Emotes.AliceCenturion}";
            }
        }
    }
}
