using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using izolabella.Google.Classes.OAuth2.Helpers;
using izolabella.OpenWeatherMap.NET;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Snapshot.Objects.Util;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot
{
    public class MercurySnapshotProgram
    {
        public MercurySnapshotProgram(StartupItems StartupItems)
        {
            this.StartupItems = StartupItems;
            this.DiscordSocketClient = new(Configurations.DiscordConfig);
            this.OpenWeatherMapClient = new();
        }

        public StartupItems StartupItems { get; }
        public DiscordSocketClient DiscordSocketClient { get; private set; }
        public OpenWeatherMapClient OpenWeatherMapClient { get; private set; }
        public GoogleOAuth2Handler? GoogleOAuth2 { get; private set; }

        public void StartProgram()
        {
            this.DiscordSocketClient = new(Configurations.DiscordConfig);
            this.OpenWeatherMapClient = new(this.StartupItems.OpenWeatherMapAppId, UnitTypes.Imperial);
            this.GoogleOAuth2 = new(new Uri(Strings.MercuryBaseUrl), this.StartupItems.GoogleClientSecrets, new(Strings.GoogleFileDatastoreLocation, false), Strings.MercuryGoogleRedirectUrl, Strings.MercuryGoogleRedirectUrl, GoogleClient.Scopes);
            this.GoogleOAuth2.TokenPOSTed += this.GoogleOAuth2_TokenPOSTed;
        }

        private void GoogleOAuth2_TokenPOSTed(UserCredential UserCredential, TokenResponse TokenResponse, izolabella.Google.Classes.OAuth2.AuthorizationRegister OriginalCall)
        {
            IRecord<TokenResponse> CurrentRecord = Registers.GoogleCredentialsRegister.GetRecord(OriginalCall.ApplicationAppliedTag) ?? new Record<TokenResponse>(TokenResponse, new List<string>());
            CurrentRecord.ObjectToStore.AccessToken = TokenResponse.AccessToken;
            if (TokenResponse.RefreshToken != null)
            {
                CurrentRecord.ObjectToStore.RefreshToken = TokenResponse.RefreshToken;
            }
            Registers.GoogleCredentialsRegister.SaveRecord(OriginalCall.ApplicationAppliedTag, CurrentRecord);
        }

        public static StartupItems? GetStartupItems()
        {
            IRecord<StartupItems>? Items = Registers.MercuryStartupItemsRegister.GetRecord(Strings.MercuryStartupItemsKey);
            return Items?.ObjectToStore;
        }
    }
}
