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
using Mercury.Snapshot.Objects.Structures.Personalization;
using Google.Apis.Auth.OAuth2.Responses;
using Mercury.Unification.IO.File;
using izolabella.Google.Classes.Consts;

namespace Mercury.Snapshot.Objects.Util.Google.General
{
    public class GoogleApp
    {
        public static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly, SheetsService.Scope.SpreadsheetsReadonly };
        public static readonly string ApplicationName = "MercuryDOTSnapshot";


        public GoogleApp(ulong UserId)
        {
            UserCredential? Credential = this.AuthorizeAndRepairAsync().Result;
            if (Credential != null)
            {
                this.calendarManager = new(Credential);
                this.sheetsManager = new(Credential);
            }

            this.UserId = UserId;
        }



        private GoogleCalendar? calendarManager;
        public GoogleCalendar? CalendarManager => this.calendarManager;



        private GoogleSheetsManager? sheetsManager;
        public GoogleSheetsManager? SheetsManager => this.sheetsManager;

        public ulong UserId { get; }
        private UserCredential? LastUserCredential { get; set; }
        public bool IsAuthenticated
        {
            get
            {
                return  (this.LastUserCredential == null || this.LastUserCredential.Token.IsExpired(new Clock())) 
                    &&
                        (this.calendarManager != null && this.SheetsManager != null);
            }
        }
        public async Task<UserCredential?> AuthorizeAndRepairAsync()
        {
            Record<TokenResponse>? Record = Registers.GoogleCredentialsRegister.GetRecord<TokenResponse>(this.UserId.ToString());
            if(Record != null)
            {
                UserCredential C = await Program.GoogleOAuth2Handler.GetUserCredentialFromTokenResponseAsync(Record.ObjectToStore);
                this.LastUserCredential = C;
                this.sheetsManager = new(C);
                this.calendarManager = new(C);
                return C;
            }
            return null;
        }
    }
}
