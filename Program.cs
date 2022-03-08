global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;

global using Discord.WebSocket;
global using Discord;

using openweathermap.NET;

using Google.Apis.Calendar.v3.Data;
using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Snapshot.Objects.Util.Cards;
using Mercury.Snapshot.Objects.Util.Google.General;
using izolabella.Discord;
using izolabella.Discord.Commands.Attributes;

namespace Mercury.Snapshot
{
    public class Program
    {
        internal static readonly DiscordSocketClient Client = new();
        internal static readonly OpenWeatherMapClient OpenWeatherMapClient = new();
        internal static readonly GoogleApp GoogleClient = new();
        internal static readonly MercuryProfile MercuryUser = new(528750326107602965, new("1f6vea1vR4MQ9ts88cebJPhxEc675pyX7St32DJyo7Cg", "primary"));
        internal static readonly DiscordWrapper DiscordWrapper = new(Client);

        public static void Main()
        {
            OpenWeatherMapClient.AppId = File.ReadAllText("OpenWeatherMap App Id.txt");
            MainAsync().GetAwaiter().GetResult();
        }
        public static async Task MainAsync()
        {
            await Client.LoginAsync(TokenType.Bot, File.ReadAllText("Discord Token.txt"));
            await Client.StartAsync();
            await Task.Delay(3000);
            DiscordWrapper.CommandHandler.AllowBotInteractions = false;
            DiscordWrapper.CommandHandler.CommandNeedsValidation += (SocketMessage Message, CommandAttribute Attr) =>
            {
                return Message.MentionedUsers.Any(User => User.Id == Client.CurrentUser.Id) && Attr.Tags.Any(Tag => Message.Content.ToLower().Contains(Tag.ToLower()));
            };
            await DiscordWrapper.CommandHandler.StartReceiving();
            
            await Task.Delay(-1);
        }
    }
}