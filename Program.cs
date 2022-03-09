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
using OpenWeatherMap.NET;

namespace Mercury.Snapshot
{
    public class Program
    {
        private static readonly DiscordSocketClient client = new();
        public static DiscordSocketClient DiscordClient => client;



        private static readonly DiscordWrapper discordWrapper = new(DiscordClient);

        public static DiscordWrapper DiscordWrapper => discordWrapper;



        private static readonly OpenWeatherMapClient openWeatherMapClient = new();
        public static OpenWeatherMapClient OpenWeatherMapClient => openWeatherMapClient;



        private static readonly MercuryProfile mercuryUser = new(528750326107602965);
        public static MercuryProfile MercuryUser => mercuryUser;



        private static readonly GoogleApp googleClient = new();
        public static GoogleApp GoogleClient
        {
            get
            {
                GoogleApp.GetUserCredential();
                return googleClient;
            }
        }

        public static void Main()
        {
            GoogleClient.SheetsManager.ToString();
            OpenWeatherMapClient.AppId = File.ReadAllText("OpenWeatherMap App Id.txt");
            MainAsync().GetAwaiter().GetResult();
        }
        public static async Task MainAsync()
        {
            await DiscordClient.LoginAsync(TokenType.Bot, File.ReadAllText("Discord Token.txt"));
            await DiscordClient.StartAsync();
            await Task.Delay(3000);
            DiscordWrapper.CommandHandler.AllowBotInteractions = false;
            DiscordWrapper.CommandHandler.CommandNeedsValidation += (SocketMessage Message, CommandAttribute Attr) =>
            {
                return Message.MentionedUsers.Any(User => User.Id == DiscordClient.CurrentUser.Id) && Attr.Tags.Any(Tag => Message.Content.ToLower().Contains(Tag.ToLower()));
            };
            await DiscordWrapper.CommandHandler.StartReceiving();
            await Task.Delay(-1);
        }
    }
}