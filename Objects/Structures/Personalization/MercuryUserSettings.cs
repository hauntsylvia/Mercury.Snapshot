using Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers;
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
        internal MercuryUserSettings(GoogleCalendarSettings GoogleCalendarSettings, GoogleSheetsSettings GoogleSheetsSettings)
        {
            this.googleCalendarSettings = GoogleCalendarSettings;
            this.googleSheetsSettings = GoogleSheetsSettings;
        }

        internal MercuryUserSettings()
        {
            this.googleCalendarSettings = new();
            this.googleSheetsSettings = new(null);
        }


        [JsonProperty("GoogleCalendarSettings")]
        private readonly GoogleCalendarSettings googleCalendarSettings;
        internal GoogleCalendarSettings GoogleCalendarSettings => this.googleCalendarSettings;


        [JsonProperty("GoogleSheetsSettings")]
        private readonly GoogleSheetsSettings googleSheetsSettings;
        internal GoogleSheetsSettings GoogleSheetsSettings => this.googleSheetsSettings;
    }
}
