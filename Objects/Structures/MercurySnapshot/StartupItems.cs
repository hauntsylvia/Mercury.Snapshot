using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.MercurySnapshot
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StartupItems
    {
        [JsonConstructor]
        public StartupItems(string OpenWeatherMapAppId, string DiscordToken, UserCredential GoogleServerCredentials, ClientSecrets GoogleClientSecrets)
        {
            this.OpenWeatherMapAppId = OpenWeatherMapAppId;
            this.DiscordToken = DiscordToken;
            this.GoogleServerCredentials = GoogleServerCredentials;
            this.GoogleClientSecrets = GoogleClientSecrets;
        }

        [JsonProperty("OpenWeatherMapAppId")]
        public string OpenWeatherMapAppId { get; }

        [JsonProperty("DiscordAuthorization")]
        public string DiscordToken { get; }

        [JsonProperty("GoogleServerCredentials")]
        public UserCredential GoogleServerCredentials { get; }

        [JsonProperty("GoogleClientSecrets")]
        public ClientSecrets GoogleClientSecrets { get; }
    }
}
