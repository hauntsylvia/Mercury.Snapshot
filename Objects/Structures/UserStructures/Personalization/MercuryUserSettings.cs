using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization.Peronalizers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MercuryUserSettings
    {
        [JsonConstructor]
        public MercuryUserSettings(GoogleCalendarSettings? GoogleCalendarSettings,
                                   GoogleSheetsSettings? GoogleSheetsSettings,
                                   WeatherSettings? WeatherSettings,
                                   CultureSettings? CultureSettings)
        {
            this.GoogleCalendarSettings = GoogleCalendarSettings ?? new();
            this.GoogleSheetsSettings = GoogleSheetsSettings ?? new(null);
            this.WeatherSettings = WeatherSettings ?? new(null);
            this.CultureSettings = CultureSettings ?? new();
        }

        public MercuryUserSettings()
        {
            this.GoogleCalendarSettings = new();
            this.GoogleSheetsSettings = new(null);
            this.WeatherSettings = new(null);
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
