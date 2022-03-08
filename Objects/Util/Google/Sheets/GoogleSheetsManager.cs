using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using Mercury.Snapshot.Objects.Structures.Financial;
using Mercury.Snapshot.Objects.Util.Google.General;

namespace Mercury.Snapshot.Objects.Util.Google.Sheets
{
    internal class GoogleSheetsManager
    {
        internal GoogleSheetsManager()
        {
            this.Service = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleApp.GetUserCredential(),
                ApplicationName = GoogleApp.ApplicationName,
            });
        }


        private SheetsService Service { get; set; }


        internal decimal? GetUserBalance(string SpreadsheetId)
        {
            SpreadsheetsResource.ValuesResource.GetRequest GetCells = this.Service.Spreadsheets.Values.Get(SpreadsheetId, "Expenditure!G2");
            ValueRange Response = GetCells.Execute();
            IList<IList<object>> Values = Response.Values;
            if (Values != null && Values.Count > 0 && decimal.TryParse(((string)Values[0][0]).Remove(((string)Values[0][0]).LastIndexOf('$'), 1), out decimal Amount))
                return Amount;
            return null;
        }


        internal IReadOnlyList<Expenditure> GetUserExpenditures(string SpreadsheetId)
        {
            SpreadsheetsResource.ValuesResource.GetRequest GetCells = this.Service.Spreadsheets.Values.Get(SpreadsheetId, "Expenditure!A:D");
            ValueRange Response = GetCells.Execute();
            IList<IList<object>> Values = Response.Values;
            List<Expenditure> Expenditures = new();
            if (Values != null && Values.Count > 0)
                foreach (IList<object> Row in Values)
                    if (DateTime.TryParse((string)Row[0], out DateTime Timestamp) && decimal.TryParse(((string)Row[1]).Remove(((string)Row[1]).LastIndexOf('$'), 1), out decimal Amount))
                        Expenditures.Add(new(Timestamp, Amount, (string)Row[2], (string)Row[3]));
            return Expenditures;
        }
    }
}
