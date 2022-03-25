using Google.Apis.Auth.OAuth2;
using izolabella.ConsoleHelper;
using Mercury.Snapshot.Objects.Structures.Exceptions;

namespace Mercury.Snapshot.Objects.Structures.MercurySnapshot
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class StartupItems
    {
        [JsonConstructor]
        internal StartupItems(string OpenWeatherMapAppId, string DiscordToken, ClientSecrets GoogleClientSecrets, LoggingLevel DiscordWrapperLoggingLevel)
        {
            this.openWeatherMapAppId = OpenWeatherMapAppId;
            this.discordToken = DiscordToken;
            this.googleClientSecrets = GoogleClientSecrets;
            this.DiscordWrapperLoggingLevel = DiscordWrapperLoggingLevel;
        }
        internal StartupItems()
        {
        }

        /// <summary>
        /// If true, none of the credentials inside of this app are legitimate, or at least one of them is null.
        /// </summary>
        [JsonProperty("IsCold")]
        internal bool IsCold => this.openWeatherMapAppId == null || this.discordToken == null || this.googleClientSecrets == null;

        private readonly string? openWeatherMapAppId;
        [JsonProperty("OpenWeatherMapAppId")]
        internal string OpenWeatherMapAppId => this.openWeatherMapAppId ?? throw new MercuryColdStartupException(nameof(this.OpenWeatherMapAppId));

        private readonly string? discordToken;
        [JsonProperty("DiscordAuthorization")]
        internal string DiscordToken => this.discordToken ?? throw new MercuryColdStartupException(nameof(this.DiscordToken));

        private readonly ClientSecrets? googleClientSecrets;
        [JsonProperty("GoogleClientSecrets")]
        internal ClientSecrets GoogleClientSecrets => this.googleClientSecrets ?? throw new MercuryColdStartupException(nameof(this.GoogleClientSecrets));

        [JsonProperty("DiscordWrapperLoggingLevel")]
        internal LoggingLevel DiscordWrapperLoggingLevel { get; } = LoggingLevel.All;
    }
}
