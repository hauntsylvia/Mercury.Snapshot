
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;

namespace Mercury.Snapshot.Objects.Util.Managers
{
    public class GoogleSheetsManager
    {
        public GoogleSheetsManager(UserCredential Credential)
        {
            this.Service = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = GoogleClient.ApplicationName,
            });
        }

        private SheetsService Service { get; set; }

        public decimal? GetUserBalance(string SpreadsheetId)
        {
            SpreadsheetsResource.ValuesResource.GetRequest GetCells = this.Service.Spreadsheets.Values.Get(SpreadsheetId, "Expenditure!G2");
            ValueRange Response = GetCells.Execute();
            IList<IList<object>> Values = Response.Values;
            return Values != null && Values.Count > 0 && decimal.TryParse(((string)Values[0][0]).Remove(((string)Values[0][0]).LastIndexOf('$'), 1), out decimal Amount)
                ? Amount
                : null;
        }

        public IReadOnlyList<MercuryExpenditureEntry> GetUserExpenditures(string SpreadsheetId)
        {
            SpreadsheetsResource.ValuesResource.GetRequest GetCells = this.Service.Spreadsheets.Values.Get(SpreadsheetId, "Expenditure!A:D");
            ValueRange Response = GetCells.Execute();
            IList<IList<object>> Values = Response.Values;
            List<MercuryExpenditureEntry> Expenditures = new();
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

            return Expenditures;
        }
    }
}
