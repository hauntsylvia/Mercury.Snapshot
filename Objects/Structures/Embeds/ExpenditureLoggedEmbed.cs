using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using System.Globalization;

namespace Mercury.Snapshot.Objects.Structures.Embeds
{
    public class ExpenditureLoggedEmbed : EmbedBuilder
    {
        public ExpenditureLoggedEmbed(ExpenditureEntry Entry, CultureInfo Culture)
        {
            this.Color = EmbedDefaults.EmbedColor;
            this.Description = $"`${Math.Abs(Entry.DollarAmount).ToString(Culture)}` {(Entry.DollarAmount < 0 ? "paid to" : "paid from")} `{Entry.PayeeOrPayer}`";
            this.Title = Entry.Category;
            this.Timestamp = Entry.Timestamp;
            this.Footer = new()
            {
                Text = "Timestamp of expenditure"
            };
        }
    }
}
