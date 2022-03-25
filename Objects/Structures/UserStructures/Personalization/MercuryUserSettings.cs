using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class MercuryUserSettings
    {
        [JsonConstructor]
        internal MercuryUserSettings(GoogleCalendarSettings GoogleCalendarSettings, GoogleSheetsSettings GoogleSheetsSettings, WeatherSettings WeatherSettings)
        {
            this.GoogleCalendarSettings = GoogleCalendarSettings;
            this.GoogleSheetsSettings = GoogleSheetsSettings;
            this.WeatherSettings = WeatherSettings;
        }

        internal MercuryUserSettings()
        {
            this.GoogleCalendarSettings = new();
            this.GoogleSheetsSettings = new(null);
            this.WeatherSettings = new("04614");
        }

        [JsonProperty("GoogleCalendarSettings")]
        internal GoogleCalendarSettings GoogleCalendarSettings { get; }

        [JsonProperty("GoogleSheetsSettings")]
        internal GoogleSheetsSettings GoogleSheetsSettings { get; }

        [JsonProperty("WeatherSettings")]
        internal WeatherSettings WeatherSettings { get; }
    }
}
