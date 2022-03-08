using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Personalization
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class MercuryUserSettings
    {
        [JsonConstructor]
        internal MercuryUserSettings(string? GoogleExpenditureSpreadsheetId, string? GoogleCalendarId)
        {
            this.googleExpenditureSpreadsheetId = GoogleExpenditureSpreadsheetId;
            this.googleCalendarId = GoogleCalendarId;
        }


        [JsonProperty("GoogleExpenditureSpreadsheetId")]
        private readonly string? googleExpenditureSpreadsheetId;
        internal string? GoogleExpenditureSpreadsheetId => this.googleExpenditureSpreadsheetId;


        [JsonProperty("GoogleCalendarId")]
        private readonly string? googleCalendarId;
        internal string? GoogleCalendarId => this.googleCalendarId;
    }
}
