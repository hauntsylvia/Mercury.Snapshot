using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public class ExpendituresCard : ICard
    {
        public ExpendituresCard()
        {
        }

        public async Task<IReadOnlyList<EmbedFieldBuilder>> RenderAsync(MercuryUser Profile)
        {
            string? Id = Profile.Settings.ObjectToStore.GoogleSheetsSettings.ExpenditureSpreadsheetId;
            if (Id != null && Profile.GoogleClient.IsAuthenticated && Profile.GoogleClient.SheetsManager != null)
            {
                DateTime ThisMonth = new(DateTime.Now.Year, DateTime.Now.Month, 1);
                IReadOnlyCollection<ExpenditureEntry> Expenditures = await Profile.GoogleClient.SheetsManager.GetExpenditures(ThisMonth, ThisMonth.AddMonths(1), int.MaxValue);
                Dictionary<string, double> Counting = new();
                foreach (ExpenditureEntry Expenditure in Expenditures)
                {
                    if (Expenditure.PayeeOrPayer.Length > 0 && Expenditure.Timestamp.Month == DateTime.Now.Month)
                    {
                        if (Counting.ContainsKey(Expenditure.PayeeOrPayer))
                        {
                            Counting[Expenditure.PayeeOrPayer] += Expenditure.DollarAmount;
                        }
                        else
                        {
                            Counting.Add(Expenditure.PayeeOrPayer, Expenditure.DollarAmount);
                        }
                    }
                }
                Dictionary<string, double> Enum = Counting.OrderBy(E => E.Value).ToDictionary(Key => Key.Key, Value => Value.Value);
                return new List<EmbedFieldBuilder>()
                {
                    {
                        new EmbedFieldBuilder()
                        {
                            Name = $"Current Balance",
                            Value = $"${Profile.GoogleClient.SheetsManager.GetUserBalance(Id)}\n\u200b"
                        }
                    },
                    {
                        new EmbedFieldBuilder()
                        {
                            Name = $"The Most Soul-Sucking Companies",
                            Value = $"_. . according to your bank account this month._"
                        }
                    },
                    {
                        new EmbedFieldBuilder()
                        {
                            Name = $"{Enum.First().Key}",
                            Value = $"${Enum.First().Value}"
                        }
                    },
                    {
                        new EmbedFieldBuilder()
                        {
                            Name = $"{Enum.ElementAt(1).Key}",
                            Value = $"${Enum.ElementAt(1).Value}"
                        }
                    },
                    {
                        new EmbedFieldBuilder()
                        {
                            Name = $"{Enum.ElementAt(2).Key}",
                            Value = $"${Enum.ElementAt(2).Value}"
                        }
                    }
                };
            }
            else
            {
                return new List<EmbedFieldBuilder>();
            }
        }
    }
}
