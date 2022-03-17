global using Discord;
global using Discord.WebSocket;
global using Mercury.Snapshot.Consts;
global using Newtonsoft.Json;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using izolabella.Discord;
using izolabella.Google.Classes.OAuth2.Helpers;
using izolabella.OpenWeatherMap.NET;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Snapshot.Objects.Util;
using Mercury.Unification.IO.File.Records;

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
            FileInfo OpenWeatherMapCredentialsFile = new("OpenWeatherMap App Id.txt");
            if (OpenWeatherMapCredentialsFile.Exists)
            {
                using StreamReader Stream = new(OpenWeatherMapCredentialsFile.FullName);
                OpenWeatherMapClient.AppId = Stream.ReadToEnd();
                ClientSecrets? Secrets = null;
                using FileStream FileStream = new("Google Credentials.json", FileMode.Open, FileAccess.Read);
                Secrets = GoogleClientSecrets.FromStream(FileStream).Secrets;
                if (Secrets != null)
                {
                    string Redirect = "https://mercury-bot.ml:443/google-oauth2/GoogleAuthReceiver/";
                    string TokenPath = "Google Cache";
                    googleOAuth2Handler = new(new Uri("https://mercury-bot.ml:443/"), Secrets, new FileDataStore(TokenPath, true), Redirect, Redirect, GoogleClient.Scopes);
                    await DiscordClient.LoginAsync(TokenType.Bot, File.ReadAllText("Discord Token.txt"));
                    await DiscordClient.StartAsync();
                    await Task.Delay(3000);
                    DiscordWrapper.CommandHandler.AllowBotInteractions = false;
                    await DiscordWrapper.CommandHandler.StartReceiving();
                    DiscordWrapper.CommandHandler.CommandNeedsValidation = (SlashCommand, Attr) =>
                    {
                        MercuryProfile P = new(SlashCommand.User.Id);
                        P.GoogleClient.AuthorizeAndRepairAsync().GetAwaiter().GetResult();
                        return true;
                    };
                    GoogleOAuth2Handler.TokenPOSTed += (UserCredential, TokResponse, OriginalCall) =>
                    {
                        IRecord<TokenResponse> CurrentRecord = Registers.GoogleCredentialsRegister.GetRecord(OriginalCall.ApplicationAppliedTag) ?? new Record<TokenResponse>(TokResponse, new List<string>());
                        CurrentRecord.ObjectToStore.AccessToken = TokResponse.AccessToken;
                        if (TokResponse.RefreshToken != null)
                        {
                            CurrentRecord.ObjectToStore.RefreshToken = TokResponse.RefreshToken;
                        }

                        Registers.GoogleCredentialsRegister.SaveRecord(OriginalCall.ApplicationAppliedTag, CurrentRecord);
                    };
                }
            }
            await Task.Delay(-1);
        }
    }
}