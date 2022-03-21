using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class ExpenditureLoggedEmbed : EmbedBuilder
    {
        public ExpenditureLoggedEmbed(IExpenditureEntry Entry)
        {
            this.Description = $"`${Math.Abs(Entry.DollarAmount)}` {(Entry.DollarAmount < 0 ? "paid to" : "paid from")} `{Entry.PayeeOrPayer}`";
            this.Title = Entry.Category;
            this.Timestamp = Entry.Timestamp;
            this.WithFooter("Timestamp of expenditure");
        }
    }
}
