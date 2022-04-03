using Google.Apis.Auth.OAuth2;
using izolabella.ConsoleHelper;
using Mercury.Snapshot.Objects.Structures.Exceptions;

namespace Mercury.Snapshot.Objects.Structures.MercurySnapshot
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StartupItems
    {
        [JsonConstructor]
        public StartupItems(string OpenWeatherMapAppId, string DiscordToken, ClientSecrets GoogleClientSecrets, LoggingLevel DiscordWrapperLoggingLevel)
        {
            this.OpenWeatherMapAppId = OpenWeatherMapAppId;
            this.DiscordToken = DiscordToken;
            this.GoogleClientSecrets = GoogleClientSecrets;
            this.DiscordWrapperLoggingLevel = DiscordWrapperLoggingLevel;
        }

        [JsonProperty("OpenWeatherMapAppId")]
        public string OpenWeatherMapAppId { get; }

        [JsonProperty("DiscordAuthorization")]
        public string DiscordToken { get; }

        [JsonProperty("GoogleClientSecrets")]
        public ClientSecrets GoogleClientSecrets { get; }

        [JsonProperty("DiscordWrapperLoggingLevel")]
        public LoggingLevel DiscordWrapperLoggingLevel { get; } = LoggingLevel.All;
    }
}
