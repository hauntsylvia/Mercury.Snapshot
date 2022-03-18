global using Discord;
global using Discord.WebSocket;
global using Mercury.Snapshot.Consts;
global using Mercury.Snapshot.Objects.Util.Discord;
global using Newtonsoft.Json;
using Google.Apis.Auth.OAuth2;
using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Snapshot.Objects.Util.HighTier.Initializers;
using Mercury.Snapshot.Objects.Util.HighTier.Programs;
using Mercury.Snapshot.Objects.Util.Managers;

namespace Mercury.Snapshot
{
    public class Program
    {
        public static MercurySnapshotProgram CurrentApp { get; set; } = new(new MercurySnapshotInitializer(GetStartupItems(null)));
        public static StartupItems GetStartupItems(StartupItems? Items = null)
        {
            StartupItemsManager ConfigManager = new(Registers.MercuryStartupItemsRegister, Strings.MercuryStartupItemsKey);
            Items ??= ConfigManager.GetStartupItems();
            if (Items != null)
            {
                ConfigManager.SaveStartupItems(Items);
                return Items;
            }
            else if (File.Exists(Strings.GoogleCredentialsFileLocation))
            {
                Console.WriteLine("Input OpenWeatherMap application id.");
                string? OpenWeatherMapAppId = Console.ReadLine();
                Console.WriteLine("Input Discord token.");
                string? DiscordAuthorization = Console.ReadLine();
                using FileStream FileStream = new(Strings.GoogleCredentialsFileLocation, FileMode.Open, FileAccess.Read);
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
            await CurrentApp.Initializer.GoogleOAuth2.StopListener();
            StartupItems Items = GetStartupItems(null);
            CurrentApp = new(new MercurySnapshotInitializer(Items));
            CurrentApp.RunProgram();
            await Task.Delay(-1);
        }
    }
}