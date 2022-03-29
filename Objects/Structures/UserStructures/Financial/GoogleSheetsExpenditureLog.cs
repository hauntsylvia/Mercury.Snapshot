
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Snapshot.Objects.Util;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    public class GoogleSheetsExpenditureLog : IExpenditureLog
    {
        public GoogleSheetsExpenditureLog(UserCredential Credential, MercuryUser User)
        {
            this.Service = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = GoogleClient.ApplicationName,
            });
            this.User = User;
        }

        public string? SpreadsheetId => this.User.Settings.GoogleSheetsSettings.ExpenditureSpreadsheetId;
        private SheetsService Service { get; set; }
        public MercuryUser User { get; }

        private static object[] GetFormatForEntries(ExpenditureEntry Entry)
        {
            return new object[] { Entry.Timestamp.ToShortDateString(), $"{Entry.DollarAmount}", Entry.PayeeOrPayer, Entry.Category, Entry.Id };
        }

        private static ExpenditureEntry? GetExpenditureEntryFromFormat(object[] Row)
        {
            if (Row.Length > 3 && DateTime.TryParse((string)Row[0], out DateTime Timestamp) && double.TryParse(((string)Row[1]), out double Amount))
            {
                ExpenditureEntry E = new(Timestamp, Amount, (string)Row[2], (string)Row[3], Origins.Google, Row.Length > 4 && ulong.TryParse(Row[4].ToString(), out ulong Id) ? Id : Identifier.GetIdentifier());
                return E;
            }
            return null;
        }

        public async Task<IReadOnlyCollection<ExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            List<ExpenditureEntry> Expenditures = new();
            if (this.SpreadsheetId != null)
            {
                SpreadsheetsResource.ValuesResource.GetRequest GetCells = this.Service.Spreadsheets.Values.Get(this.SpreadsheetId, "Expenditure!A:E");
                ValueRange Response = await GetCells.ExecuteAsync();
                IList<IList<object>> Rows = Response.Values;
                ValueRange ValRange = new()
                {
                    Values = new List<IList<object>>()
                };
                if (Rows != null && Rows.Count > 0)
                {
                    int Index = 0;
                    foreach (IList<object> Row in Rows)
                    {
                        ValRange.Values.Add(new List<object>());
                        ExpenditureEntry? E = GetExpenditureEntryFromFormat(Row.ToArray());
                        if (E != null)
                        {
                            Expenditures.Add(E);
                            ((List<object>)ValRange.Values[Index]).AddRange(GetFormatForEntries(E));
                            ValRange.Range = $"Expenditure!{2}:{2 + Index}";
                            Index++;
                        }
                    }
                }
                List<IList<ExpenditureEntry>> Ordered = new();
                for (int Index = 0; Index < ValRange.Values.Count; Index++)
                {
                    IList<object> O = (List<object>)ValRange.Values[Index];
                    Ordered.Add(new List<ExpenditureEntry>());
                    ExpenditureEntry? ExpEntry = GetExpenditureEntryFromFormat(O.ToArray());
                    if (ExpEntry != null)
                    {
                        Ordered[Index].Add(ExpEntry);
                    }
                }
                for(int Index = 0; Index < Ordered.Count; Index++)
                {
                    Ordered[Index] = Ordered[Index].OrderBy(Entry => Entry.Timestamp).ToList();
                }
                await this.Service.Spreadsheets.Values.BatchUpdate(new()
                {
                    Data = new List<ValueRange>() { ValRange },
                    ValueInputOption = "USER_ENTERED",
                    IncludeValuesInResponse = true,
                }, this.SpreadsheetId).ExecuteAsync();
            }

            return Expenditures;
        }

        public async Task SaveExpenditures(params ExpenditureEntry[] Entries)
        {
            if(this.SpreadsheetId != null)
            {
                int LastRow = (await this.GetExpenditures(DateTime.MinValue, DateTime.MaxValue, int.MaxValue)).Count;
                List<IList<object>> NewVals = new();
                foreach (ExpenditureEntry Entry in Entries)
                {
                    List<object> ThisRow = new();
                    ThisRow.AddRange(GetFormatForEntries(Entry));
                    NewVals.Add(ThisRow);
                }
                await this.Service.Spreadsheets.Values.BatchUpdate(new()
                {
                    Data = new List<ValueRange>() { new() { Values = NewVals, Range = $"Expenditure!{LastRow}:{NewVals.Count + LastRow}" } },
                    ValueInputOption = "RAW",
                    IncludeValuesInResponse = true,
                }, this.SpreadsheetId).ExecuteAsync();
            }
        }

        public double? GetUserBalance(string SpreadsheetId)
        {
            SpreadsheetsResource.ValuesResource.GetRequest GetCells = this.Service.Spreadsheets.Values.Get(SpreadsheetId, "Expenditure!G2");
            ValueRange Response = GetCells.Execute();
            IList<IList<object>> Values = Response.Values;
            return Values != null && Values.Count > 0 && double.TryParse(((string)Values[0][0]).Remove(((string)Values[0][0]).LastIndexOf('$'), 1), out double Amount)
                ? Amount
                : null;
        }
    }
}
