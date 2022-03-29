using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using izolabella.Discord;
using izolabella.Google.Classes.OAuth2.Helpers;
using izolabella.OpenWeatherMap.NET;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Objects.Util.HighTier.Initializers
{
    public class MercurySnapshotInitializer
    {
        public MercurySnapshotInitializer(StartupItems StartupItems)
        {
            this.StartupItems = StartupItems;
            this.DiscordSocketClient = new(new(Configurations.DiscordConfig), StartupItems.DiscordWrapperLoggingLevel);
            this.OpenWeatherMapClient = new(StartupItems.OpenWeatherMapAppId, UnitTypes.Metric);
            this.GoogleOAuth2 = new(new Uri(Strings.MercuryBaseUrl), StartupItems.GoogleClientSecrets, new(Strings.GoogleStrings.GoogleFileDatastoreLocation, false), Strings.GoogleStrings.MercuryGoogleRedirectUrl, Strings.GoogleStrings.MercuryGoogleRedirectUrl, Strings.GoogleStrings.Scopes);
            this.GoogleOAuth2.TokenPOSTed += this.GoogleOAuth2_TokenPOSTed;
        }

        public StartupItems StartupItems { get; }
        public DiscordWrapper DiscordSocketClient { get; private set; }
        public OpenWeatherMapClient OpenWeatherMapClient { get; private set; }
        public GoogleOAuth2Handler GoogleOAuth2 { get; private set; }

        private void GoogleOAuth2_TokenPOSTed(UserCredential UserCredential, TokenResponse TokenResponse, izolabella.Google.Classes.OAuth2.AuthorizationRegister OriginalCall)
        {
            Record<TokenResponse> CurrentRecord = Registers.GoogleCredentialsRegister.GetRecord(OriginalCall.ApplicationAppliedTag) ?? new Record<TokenResponse>(TokenResponse);
            CurrentRecord.ObjectToStore.AccessToken = TokenResponse.AccessToken;
            if (TokenResponse.RefreshToken != null)
            {
                CurrentRecord.ObjectToStore.RefreshToken = TokenResponse.RefreshToken;
            }
            Registers.GoogleCredentialsRegister.SaveRecord(OriginalCall.ApplicationAppliedTag, CurrentRecord);
        }
    }
}
