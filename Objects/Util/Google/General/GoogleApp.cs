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
using Mercury.Snapshot.Objects.Structures.Mercury.Calendars;

namespace Mercury.Snapshot.Objects.Util.Google.General
{
    public class GoogleApp
    {
        public static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly, SheetsService.Scope.SpreadsheetsReadonly };
        public static readonly string ApplicationName = "MercuryDOTSnapshot";


        public GoogleApp()
        {
            this.calendarManager = new();
            this.sheetsManager = new();
        }



        private readonly GoogleCalendarManager calendarManager;
        public GoogleCalendarManager CalendarManager => this.calendarManager;



        private readonly GoogleSheetsManager sheetsManager;
        public GoogleSheetsManager SheetsManager => this.sheetsManager;



        public static UserCredential GetUserCredential()
        {
            UserCredential Credential;
            using (FileStream stream = new("Google Credentials.json", FileMode.Open, FileAccess.Read))
                Credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(Path.Combine(Unification.IO.File.Register.DefaultLocation.FullName, "Google"), false)).Result;
            return Credential;
        }
    }
}
