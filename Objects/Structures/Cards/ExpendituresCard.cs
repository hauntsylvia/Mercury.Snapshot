using Mercury.Snapshot.Objects.Structures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public class ExpendituresCard : ICard
    {
        public ExpendituresCard()
        {
        }

        public IReadOnlyList<EmbedFieldBuilder> Render(MercuryProfile Profile)
        {
            string? Id = Profile.Settings.ObjectToStore.GoogleSheetsSettings.ExpenditureSpreadsheetId;
            if (Id != null && Profile.GoogleClient.IsAuthenticated && Profile.GoogleClient.SheetsManager != null)
            {
                IReadOnlyList<Expenditure> Expenditures = Profile.GoogleClient.SheetsManager.GetUserExpenditures(Id);
                Dictionary<string, decimal> Counting = new();
                foreach (Expenditure Expenditure in Expenditures)
                {
                    if (Expenditure.PayeeOrPayer.Length > 0 && Expenditure.Timestamp.Month == DateTime.Now.Month)
                    {
                        if (Counting.ContainsKey(Expenditure.PayeeOrPayer))
                            Counting[Expenditure.PayeeOrPayer] += Expenditure.DollarAmount;
                        else
                            Counting.Add(Expenditure.PayeeOrPayer, Expenditure.DollarAmount);
                    }
                }
                Dictionary<string, decimal> Enum = Counting.OrderBy(E => E.Value).ToDictionary(Key => Key.Key, Value => Value.Value);
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
                return new List<EmbedFieldBuilder>();
        }
    }
}
