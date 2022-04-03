global using Discord;
global using Discord.WebSocket;
global using Mercury.Snapshot.Consts;
global using Newtonsoft.Json;
using Google.Apis.Auth.OAuth2;
using izolabella.ConsoleHelper;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Snapshot.Objects.Util.HighTier.Initializers;
using Mercury.Snapshot.Objects.Util.HighTier.Programs;
using Mercury.Snapshot.Objects.Util.Managers;

namespace Mercury.Snapshot
{
    public static class Program
    {
        public static MercurySnapshotProgram CurrentApp { get; set; } = new(new MercurySnapshotInitializer(GetStartupItems(null)));
        public static StartupItems GetStartupItems(StartupItems? Items = null)
        {
            StartupItemsManager ConfigManager = new(CommonRegisters.MercuryStartupItemsRegister, Strings.MercuryStrings.MercuryStartupItemsKey);
            Items ??= ConfigManager.GetStartupItems();
            if (Items != null)
            {
                ConfigManager.SaveStartupItems(Items);
                return Items;
            }
            else if (File.Exists(Strings.GoogleStrings.GoogleCredentialsFileLocation))
            {
                Console.WriteLine(Strings.MercuryStrings.InputOWMAppId);
                string? OpenWeatherMapAppId = Console.ReadLine();
                Console.WriteLine(Strings.MercuryStrings.InputDiscordToken);
                string? DiscordAuthorization = Console.ReadLine();
                using FileStream FileStream = new(Strings.GoogleStrings.GoogleCredentialsFileLocation, FileMode.Open, FileAccess.Read);
                ClientSecrets Secrets = GoogleClientSecrets.FromStream(FileStream).Secrets;
                if (OpenWeatherMapAppId != null && DiscordAuthorization != null && Secrets != null)
                {
                    Items = new(OpenWeatherMapAppId, DiscordAuthorization, Secrets, izolabella.ConsoleHelper.LoggingLevel.All);
                }
                Console.Clear();
            }
            return GetStartupItems(Items);
        }
        public static async Task Main()
        {
            await CurrentApp.Initializer.GoogleOAuth2.StopListener().ConfigureAwait(false);
            StartupItems Items = GetStartupItems(null);
            CurrentApp = new(new MercurySnapshotInitializer(Items));
            CurrentApp.RunProgram();
            await Task.Delay(-1).ConfigureAwait(false);
        }
    }
}