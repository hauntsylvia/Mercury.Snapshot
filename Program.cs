global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;

global using Discord.WebSocket;
global using Discord;

global using Mercury.Snapshot.Consts;

using Google.Apis.Calendar.v3.Data;
using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Snapshot.Objects.Util.Cards;
using Mercury.Snapshot.Objects.Util.Google.General;


using izolabella.Discord;
using izolabella.Discord.Commands.Attributes;
using izolabella.OpenWeatherMap.NET;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using System.Diagnostics;
using izolabella.Google.Classes.OAuth2.Helpers;

namespace Mercury.Snapshot
{
    public class Program
    {
        public static DiscordSocketClient DiscordClient { get; } = new(new DiscordSocketConfig()
        {
            UseSystemClock = false,
            MessageCacheSize = 20,
            AlwaysDownloadUsers = true,
            AlwaysDownloadDefaultStickers = true,
            AlwaysResolveStickers = true,
            UseInteractionSnowflakeDate = false,
        });

        public static DiscordWrapper DiscordWrapper { get; } = new(DiscordClient);

        public static OpenWeatherMapClient OpenWeatherMapClient { get; } = new();

        private static GoogleOAuth2Handler? googleOAuth2Handler;
        public static GoogleOAuth2Handler GoogleOAuth2Handler => googleOAuth2Handler ?? throw new NullReferenceException();

        public static async Task Main()
        {
            OpenWeatherMapClient.AppId = File.ReadAllText("OpenWeatherMap App Id.txt");
            ClientSecrets? Secrets = null;
            using (FileStream stream = new("Google Credentials.json", FileMode.Open, FileAccess.Read))
                Secrets = GoogleClientSecrets.FromStream(stream).Secrets;
            if (Secrets != null)
            {
                string Redirect = "https://mercury-bot.ml:443/google-oauth2/GoogleAuthReceiver/";
                string TokenPath = "Google Cache";
                googleOAuth2Handler = new(new Uri("https://mercury-bot.ml:443/"), Secrets, new FileDataStore(TokenPath, true), Redirect, Redirect, GoogleApp.Scopes);
                string B = googleOAuth2Handler.CreateAuthorizationRequest(new("cum!"));
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