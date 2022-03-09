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
        internal MercuryUserSettings(GoogleCalendarSettings GoogleCalendarSettings, GoogleSheetsSettings GoogleSheetsSettings, WeatherSettings WeatherSettings)
        {
            this.googleCalendarSettings = GoogleCalendarSettings;
            this.googleSheetsSettings = GoogleSheetsSettings;
            this.weatherSettings = WeatherSettings;
        }

        internal MercuryUserSettings()
        {
            this.googleCalendarSettings = new();
            this.googleSheetsSettings = new(null);
            this.weatherSettings = new("04614");
        }


        [JsonProperty("GoogleCalendarSettings")]
        private readonly GoogleCalendarSettings googleCalendarSettings;
        internal GoogleCalendarSettings GoogleCalendarSettings => this.googleCalendarSettings;


        [JsonProperty("GoogleSheetsSettings")]
        private readonly GoogleSheetsSettings googleSheetsSettings;
        internal GoogleSheetsSettings GoogleSheetsSettings => this.googleSheetsSettings;


        [JsonProperty("WeatherSEttings")]

        private readonly WeatherSettings weatherSettings;
        internal WeatherSettings WeatherSettings => this.weatherSettings;
    }
}
