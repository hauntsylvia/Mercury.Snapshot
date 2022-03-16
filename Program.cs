global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;

global using Discord.WebSocket;
global using Discord;

using Google.Apis.Calendar.v3.Data;
using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Snapshot.Objects.Util.Cards;
using Mercury.Snapshot.Objects.Util.Google.General;


using izolabella.Discord;
using izolabella.Discord.Commands.Attributes;
using izolabella.OpenWeatherMap.NET;
using izolabella.Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using System.Diagnostics;

namespace Mercury.Snapshot
{
    public class Program
    {
        private static readonly DiscordSocketClient client = new(new DiscordSocketConfig()
        {
            UseSystemClock = false,
            MessageCacheSize = 20,
            AlwaysDownloadUsers = true,
            AlwaysDownloadDefaultStickers = true,
            AlwaysResolveStickers = true,
            UseInteractionSnowflakeDate = false,
        });
        public static DiscordSocketClient DiscordClient => client;



        private static readonly DiscordWrapper discordWrapper = new(DiscordClient);

        public static DiscordWrapper DiscordWrapper => discordWrapper;



        private static readonly OpenWeatherMapClient openWeatherMapClient = new();
        public static OpenWeatherMapClient OpenWeatherMapClient => openWeatherMapClient;



        private static readonly MercuryProfile mercuryUser = new(528750326107602965);
        public static MercuryProfile MercuryUser => mercuryUser;

        public static async Task Main()
        {
            OpenWeatherMapClient.AppId = File.ReadAllText("OpenWeatherMap App Id.txt");
            ClientSecrets? Secrets = null;
            using (FileStream stream = new("Google Credentials.json", FileMode.Open, FileAccess.Read))
                Secrets = GoogleClientSecrets.FromStream(stream).Secrets;
            if (Secrets != null)
            {
                string Redirect = "https://mercury-bot.ml:443/google-oauth2/GoogleAuthReceiver/";
                string TokenPath = Path.Combine(Unification.IO.File.Register.DefaultLocation.FullName, "Google For Mercury");
                GoogleOAuth2Handler A = new(new Uri("https://mercury-bot.ml:443/"), Secrets, new FileDataStore(TokenPath, true), Redirect, Redirect, GoogleApp.Scopes);
                Uri B = A.CreateAuthorizationRequest(new("cum!"));
                Console.WriteLine(B);
                await DiscordClient.LoginAsync(TokenType.Bot, File.ReadAllText("Discord Token.txt"));
                await DiscordClient.StartAsync();
                await Task.Delay(3000);
                DiscordWrapper.CommandHandler.AllowBotInteractions = false;
                await DiscordWrapper.CommandHandler.StartReceiving();
            }
            await Task.Delay(-1);
        }
    }
}