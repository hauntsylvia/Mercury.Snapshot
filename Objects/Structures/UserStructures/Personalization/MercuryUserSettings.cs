using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MercuryUserSettings
    {
        [JsonConstructor]
        public MercuryUserSettings(GoogleCalendarSettings GoogleCalendarSettings,
                                   GoogleSheetsSettings GoogleSheetsSettings,
                                   WeatherSettings WeatherSettings,
                                   CultureSettings CultureSettings)
        {
            this.GoogleCalendarSettings = GoogleCalendarSettings;
            this.GoogleSheetsSettings = GoogleSheetsSettings;
            this.WeatherSettings = WeatherSettings;
            this.CultureSettings = CultureSettings;
        }

        public MercuryUserSettings()
        {
            this.GoogleCalendarSettings = new();
            this.GoogleSheetsSettings = new(null);
            this.WeatherSettings = new("04614");
            this.CultureSettings = new();
        }

        [JsonProperty("GoogleCalendarSettings")]
        public GoogleCalendarSettings GoogleCalendarSettings { get; }

        [JsonProperty("GoogleSheetsSettings")]
        public GoogleSheetsSettings GoogleSheetsSettings { get; }

        [JsonProperty("WeatherSettings")]
        public WeatherSettings WeatherSettings { get; }

        [JsonProperty("CultureSettings")]
        public CultureSettings CultureSettings { get; }
    }
}
