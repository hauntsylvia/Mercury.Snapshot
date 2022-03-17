using Google.Apis.Auth.OAuth2;
using izolabella.ConsoleHelper;
using Mercury.Snapshot.Objects.Structures.Exceptions;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.MercurySnapshot
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StartupItems
    {
        [JsonConstructor]
        public StartupItems(string OpenWeatherMapAppId, string DiscordToken, ClientSecrets GoogleClientSecrets, LoggingLevel DiscordWrapperLoggingLevel)
        {
            this.openWeatherMapAppId = OpenWeatherMapAppId;
            this.discordToken = DiscordToken;
            this.googleClientSecrets = GoogleClientSecrets;
            this.DiscordWrapperLoggingLevel = DiscordWrapperLoggingLevel;
        }
        public StartupItems()
        {
        }

        /// <summary>
        /// If true, none of the credentials inside of this app are legitimate, or at least one of them is null.
        /// </summary>
        [JsonProperty("IsCold")]
        public bool IsCold => this.openWeatherMapAppId == null || this.discordToken == null || this.googleClientSecrets== null;

        private readonly string? openWeatherMapAppId;
        [JsonProperty("OpenWeatherMapAppId")]
        public string OpenWeatherMapAppId => this.openWeatherMapAppId ?? throw new MercuryColdStartupException(nameof(this.OpenWeatherMapAppId));

        private readonly string? discordToken;
        [JsonProperty("DiscordAuthorization")]
        public string DiscordToken => this.discordToken ?? throw new MercuryColdStartupException(nameof(this.DiscordToken));

        private readonly ClientSecrets? googleClientSecrets;
        [JsonProperty("GoogleClientSecrets")]
        public ClientSecrets GoogleClientSecrets => this.googleClientSecrets ?? throw new MercuryColdStartupException(nameof(this.GoogleClientSecrets));

        [JsonProperty("DiscordWrapperLoggingLevel")]
        public LoggingLevel DiscordWrapperLoggingLevel { get; } = LoggingLevel.All;
    }
}
