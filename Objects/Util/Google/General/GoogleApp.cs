﻿
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Sheets.v4;
using Mercury.Snapshot.Objects.Structures.Mercury.Calendars;
using Mercury.Snapshot.Objects.Util.Google.Sheets;
using Mercury.Unification.IO.File;

namespace Mercury.Snapshot.Objects.Util.Google.General
{
    public class GoogleApp
    {
        public static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly, SheetsService.Scope.SpreadsheetsReadonly };
        public static readonly string ApplicationName = "MercuryDOTSnapshot";


        public GoogleApp(ulong UserId)
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
            Record<TokenResponse>? Record = Registers.GoogleCredentialsRegister.GetRecord<TokenResponse>(this.UserId.ToString());
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
