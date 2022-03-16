
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Sheets.v4;
using Mercury.Snapshot.Objects.Structures.Calendars;
using Mercury.Snapshot.Objects.Util.Managers;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Objects.Util
{
    public class GoogleClient
    {
        public static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly, SheetsService.Scope.SpreadsheetsReadonly };
        public static readonly string ApplicationName = "MercuryDOTSnapshot";


        public GoogleClient(ulong UserId)
        {
            this.UserId = UserId;
            UserCredential? Credential = this.AuthorizeAndRepairAsync().Result;
            if (Credential != null)
            {
                this.calendarManager = new(Credential);
                this.sheetsManager = new(Credential);
            }
        }



        private GoogleCalendar? calendarManager;
        public GoogleCalendar? CalendarManager => this.calendarManager;



        private GoogleSheetsManager? sheetsManager;
        public GoogleSheetsManager? SheetsManager => this.sheetsManager;

        public ulong UserId { get; }
        public bool IsAuthenticated => this.calendarManager != null && this.sheetsManager != null;
        public async Task<UserCredential?> AuthorizeAndRepairAsync()
        {
            IRecord<TokenResponse>? Record = Registers.GoogleCredentialsRegister.GetRecord(this.UserId.ToString());
            if (Record != null)
            {
                UserCredential C = await Program.GoogleOAuth2Handler.GetUserCredentialFromTokenResponseAsync(Record.ObjectToStore);
                this.sheetsManager = new(C);
                this.calendarManager = new(C);
                return C;
            }
            return null;
        }
    }
}
