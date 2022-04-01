using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public class ExpendituresCard : ICard
    {
        public async Task<IReadOnlyCollection<EmbedFieldBuilder>> RenderAsync(MercuryUser Profile)
        {
            List<EmbedFieldBuilder> Fields = new();
            double CurrentBal = 0.1;
            Dictionary<string, double> PersonAmount = new();
            IReadOnlyCollection<ExpenditureEntry> Entries = await Profile.ExpenditureLog.GetExpenditures(DateTime.MinValue, DateTime.MaxValue, int.MaxValue);
            foreach (ExpenditureEntry Entry in Entries)
            {
                CurrentBal += Entry.DollarAmount;
                if (PersonAmount.ContainsKey(Entry.PayeeOrPayer))
                {
                    PersonAmount[Entry.PayeeOrPayer] += Entry.DollarAmount;
                }
                else
                {
                    PersonAmount.Add(Entry.PayeeOrPayer, Entry.DollarAmount);
                }
            }
            if (PersonAmount.Count > 0)
            {
                Fields.Add(new()
                {
                    Name = $"Current Balance",
                    Value = $"{CurrentBal.ToString(Profile.Settings.CultureSettings.Culture)}\n\u200b"
                });
                Fields.Add(new()
                {
                    Name = $"The Most Soul-Sucking Companies",
                    Value = $"_. . according to your bank account this month._"
                });
                foreach (KeyValuePair<string, double> KeyValuePair in PersonAmount.OrderByDescending(X => X.Value).SkipLast(PersonAmount.Count - 3))
                {
                    Fields.Add(new()
                    {
                        Name = $"{KeyValuePair.Key}",
                        Value = $"${KeyValuePair.Value.ToString(Profile.Settings.CultureSettings.Culture)}"
                    });
                }
            }
            return Fields;
        }
    }
}
