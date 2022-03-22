
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Sheets.v4;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
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
                this.CalendarManager = new(Credential);
                this.SheetsManager = new(Credential);
            }
        }

        public GoogleCalendar? CalendarManager { get; private set; }

        public GoogleSheetsManager? SheetsManager { get; private set; }

        public ulong UserId { get; }
        public bool IsAuthenticated => this.CalendarManager != null && this.SheetsManager != null;
        public async Task<UserCredential?> AuthorizeAndRepairAsync()
        {
            Record<TokenResponse>? Record = Registers.GoogleCredentialsRegister.GetRecord(this.UserId.ToString());
            if (Record != null)
            {
                try
                {
                    UserCredential C = await Program.CurrentApp.Initializer.GoogleOAuth2.GetUserCredentialFromTokenResponseAsync(Record.ObjectToStore);
                    this.SheetsManager = new(C);
                    this.CalendarManager = new(C);
                    return C;
                }
                catch(Exception Ex)
                {
                    if (Ex is AggregateException || Ex is TokenResponseException)
                    {
                        Registers.GoogleCredentialsRegister.DeleteRecord(this.UserId.ToString());
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return null;
        }
    }
}
