
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Sheets.v4;
using izolabella.Google.Classes.Consts;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Snapshot.Objects.Util.Managers;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Objects.Util
{
    internal class GoogleClient
    {
        internal static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly, SheetsService.Scope.SpreadsheetsReadonly };
        internal static readonly string ApplicationName = "MercuryDOTSnapshot";

        internal GoogleClient(MercuryUser UserInstance)
        {
            this.UserInstance = UserInstance;
            UserCredential? Credential = this.AuthorizeAndRepairAsync().Result;
            if (Credential != null)
            {
                this.LastUsedCredential = Credential;
                this.CalendarManager = new(Credential);
                string? ExpSheetId = UserInstance.Settings.GoogleSheetsSettings.ExpenditureSpreadsheetId;
                this.SheetsManager = ExpSheetId != null ? new(Credential, ExpSheetId) : null;
            }
        }
        internal MercuryUser UserInstance { get; }
        internal UserCredential? LastUsedCredential { get; }
        internal bool IsAuthenticated => this.LastUsedCredential != null && !this.LastUsedCredential.Token.IsExpired(new Clock());
        internal GoogleCalendar? CalendarManager { get; private set; }
        internal GoogleSheetsExpenditureLog? SheetsManager { get; private set; }

        internal async Task<UserCredential?> AuthorizeAndRepairAsync()
        {
            Record<TokenResponse>? Record = Registers.GoogleCredentialsRegister.GetRecord(this.UserInstance.DiscordId.ToString());
            if (Record != null)
            {
                try
                {
                    UserCredential C = await Program.CurrentApp.Initializer.GoogleOAuth2.GetUserCredentialFromTokenResponseAsync(Record.ObjectToStore).ConfigureAwait(false);
                    string? ExpSheetId = this.UserInstance?.Settings.GoogleSheetsSettings.ExpenditureSpreadsheetId;
                    this.SheetsManager = ExpSheetId != null ? new(C, ExpSheetId) : null;
                    this.CalendarManager = new(C);
                    return C;
                }
                catch (Exception Ex)
                {
                    if (Ex is AggregateException || Ex is TokenResponseException)
                    {
                        Registers.GoogleCredentialsRegister.DeleteRecord(this.UserInstance.DiscordId.ToString());
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
