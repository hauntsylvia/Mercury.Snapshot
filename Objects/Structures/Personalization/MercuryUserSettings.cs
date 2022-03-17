using Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers;

namespace Mercury.Snapshot.Objects.Structures.Personalization
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MercuryUserSettings
    {
        [JsonConstructor]
        public MercuryUserSettings(GoogleCalendarSettings GoogleCalendarSettings, GoogleSheetsSettings GoogleSheetsSettings, WeatherSettings WeatherSettings)
        {
            this.GoogleCalendarSettings = GoogleCalendarSettings;
            this.GoogleSheetsSettings = GoogleSheetsSettings;
            this.WeatherSettings = WeatherSettings;
        }

        public MercuryUserSettings()
        {
            this.GoogleCalendarSettings = new();
            this.GoogleSheetsSettings = new(null);
            this.WeatherSettings = new("04614");
        }

        [JsonProperty("GoogleCalendarSettings")]
        public GoogleCalendarSettings GoogleCalendarSettings { get; }

        [JsonProperty("GoogleSheetsSettings")]
        public GoogleSheetsSettings GoogleSheetsSettings { get; }

        [JsonProperty("WeatherSettings")]
        public WeatherSettings WeatherSettings { get; }
    }
}
