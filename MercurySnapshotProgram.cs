using izolabella.Google.Classes.OAuth2.Helpers;
using izolabella.OpenWeatherMap.NET;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Snapshot.Objects.Util;
using Mercury.Unification.IO.File.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public static StartupItems? GetStartupItems()
        {
            IRecord<StartupItems>? Items = Registers.MercuryStartupItemsRegister.GetRecord(Strings.MercuryStartupItemsKey);
            return Items?.ObjectToStore;
        }
    }
}
