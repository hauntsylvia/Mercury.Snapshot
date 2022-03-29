
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Util;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    public class GoogleSheetsExpenditureLog : IExpenditureLog
    {
        public GoogleSheetsExpenditureLog(UserCredential Credential, string SpreadsheetId)
        {
            this.Service = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = GoogleClient.ApplicationName,
            });
            this.SpreadsheetId = SpreadsheetId;
        }

        public string SpreadsheetId { get; }
        private SheetsService Service { get; set; }

        public Task<IReadOnlyCollection<ExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            SpreadsheetsResource.ValuesResource.GetRequest GetCells = this.Service.Spreadsheets.Values.Get(this.SpreadsheetId, "Expenditure!A:D");
            ValueRange Response = GetCells.Execute();
            IList<IList<object>> Values = Response.Values;
            List<ExpenditureEntry> Expenditures = new();
            if (Values != null && Values.Count > 0)
            {
                foreach (IList<object> Row in Values)
                {
                    if (DateTime.TryParse((string)Row[0], out DateTime Timestamp) && double.TryParse(((string)Row[1]).Remove(((string)Row[1]).LastIndexOf('$'), 1), out double Amount))
                    {
                        Expenditures.Add(new(Timestamp, Amount, (string)Row[2], (string)Row[3], Origins.Google, Identifier.GetIdentifier()));
                    }
                }
            }

            return Task.FromResult<IReadOnlyCollection<ExpenditureEntry>>(Expenditures);
        }

        public Task SaveExpenditures(params ExpenditureEntry[] Entries)
        {
            throw new NotImplementedException();
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
