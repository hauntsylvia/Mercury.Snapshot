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
using Mercury.Snapshot.Objects.Util.Google.Sheets;
using Mercury.Snapshot.Objects.Util.Google.Calendar;

namespace Mercury.Snapshot.Objects.Util.Google.General
{
    internal class GoogleApp
    {
        internal static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly, SheetsService.Scope.SpreadsheetsReadonly };
        internal static readonly string ApplicationName = "MercuryDOTSnapshot";


        internal GoogleApp()
        {
            this.calendarManager = new();
            this.sheetsManager = new();
        }



        private readonly GoogleCalendarManager calendarManager;
        internal GoogleCalendarManager CalendarManager => this.calendarManager;



        private readonly GoogleSheetsManager sheetsManager;
        internal GoogleSheetsManager SheetsManager => this.sheetsManager;



        internal static UserCredential GetUserCredential()
        {
            UserCredential Credential;
            using (FileStream stream = new("Google Credentials.json", FileMode.Open, FileAccess.Read))
                Credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(Path.Combine(Unification.IO.File.Register.DefaultLocation.FullName, "Google"), false)).Result;
            return Credential;
        }
    }
}
