using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class ExpenditureSyncEmbed : EmbedBuilder
    {
        public ExpenditureSyncEmbed(bool Loading = true, bool Success = true)
        {
            this.Timestamp = DateTime.UtcNow;
            this.Description = Loading
                ? $"{Strings.EmbedStrings.Expenditures.ExpenditureSyncLoading} {Emotes.AliceCenturion}"
                : Success
                    ? $"{Strings.EmbedStrings.Expenditures.ExpenditureSyncCompleted}"
                    : $"{Strings.EmbedStrings.Expenditures.ExpenditureSyncFailed}";
        }
    }
}
