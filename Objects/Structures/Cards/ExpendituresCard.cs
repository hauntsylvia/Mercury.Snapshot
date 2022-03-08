using Mercury.Snapshot.Objects.Structures.Financial;
using openweathermap.NET.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    internal class ExpendituresCard : ICard
    {
        internal ExpendituresCard()
        {

        }


        public IReadOnlyList<EmbedFieldBuilder> Render()
        {
            if(Program.MercuryUser.Settings != null && Program.MercuryUser.Settings.ObjectToStore.GoogleCalendarSettings != null)
            {
                IReadOnlyList<Expenditure> Expenditures = Program.GoogleClient.SheetsManager.GetUserExpenditures(Program.MercuryUser.Settings.ObjectToStore.GoogleCalendarSettings);
                Dictionary<string, decimal> Counting = new();
                foreach(Expenditure Expenditure in Expenditures)
                {
                    if(Expenditure.PayeeOrPayer.Length > 0 && Expenditure.Timestamp.Month == DateTime.Now.Month)
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
                            Value = $"${Program.GoogleClient.SheetsManager.GetUserBalance(Program.MercuryUser.Settings.ObjectToStore.GoogleCalendarSettings)}\n\u200b"
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
