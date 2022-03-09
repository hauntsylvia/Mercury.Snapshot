using Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Personalization
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MercuryUserSettings
    {
        [JsonConstructor]
        public MercuryUserSettings(GoogleCalendarSettings GoogleCalendarSettings, GoogleSheetsSettings GoogleSheetsSettings, WeatherSettings WeatherSettings)
        {
            this.googleCalendarSettings = GoogleCalendarSettings;
            this.googleSheetsSettings = GoogleSheetsSettings;
            this.weatherSettings = WeatherSettings;
        }

        public MercuryUserSettings()
        {
            this.googleCalendarSettings = new();
            this.googleSheetsSettings = new(null);
            this.weatherSettings = new("04614");
        }


        private readonly GoogleCalendarSettings googleCalendarSettings;
        [JsonProperty("GoogleCalendarSettings")]
        public GoogleCalendarSettings GoogleCalendarSettings => this.googleCalendarSettings;


        private readonly GoogleSheetsSettings googleSheetsSettings;
        [JsonProperty("GoogleSheetsSettings")]
        public GoogleSheetsSettings GoogleSheetsSettings => this.googleSheetsSettings;



        private readonly WeatherSettings weatherSettings;
        [JsonProperty("WeatherSettings")]
        public WeatherSettings WeatherSettings => this.weatherSettings;
    }
}
