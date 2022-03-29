
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Sheets.v4;
using izolabella.Google.Classes.Consts;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Objects.Util
{
    public class GoogleClient
    {
        
        public static string ApplicationName => "MercuryDOTSnapshot";

        public GoogleClient(MercuryUser UserInstance)
        {
            this.UserInstance = UserInstance;
            UserCredential? Credential = this.AuthorizeAndRepairAsync().Result;
            if (Credential != null)
            {
                this.LastUsedCredential = Credential;
                this.CalendarManager = new(Credential);
                this.SheetsManager = new(Credential, UserInstance);
            }
        }
        public MercuryUser UserInstance { get; }
        public UserCredential? LastUsedCredential { get; }
        public bool IsAuthenticated => this.LastUsedCredential != null && !this.LastUsedCredential.Token.IsExpired(new Clock());
        public GoogleCalendar? CalendarManager { get; private set; }
        public GoogleSheetsExpenditureLog? SheetsManager { get; private set; }

        public async Task<UserCredential?> AuthorizeAndRepairAsync()
        {
            Record<TokenResponse>? Record = Registers.GoogleCredentialsRegister.GetRecord(this.UserInstance.DiscordId.ToString(this.UserInstance.Settings.CultureSettings.Culture));
            if (Record != null)
            {
                try
                {
                    UserCredential C = await Program.CurrentApp.Initializer.GoogleOAuth2.GetUserCredentialFromTokenResponseAsync(Record.ObjectToStore).ConfigureAwait(false);
                    this.SheetsManager = this.UserInstance != null ? new(C, this.UserInstance) : null;
                    this.CalendarManager = new(C);
                    return C;
                }
                catch (Exception Ex)
                {
                    if (Ex is AggregateException || Ex is TokenResponseException)
                    {
                        Registers.GoogleCredentialsRegister.DeleteRecord(this.UserInstance.DiscordId);
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
