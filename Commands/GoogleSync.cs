using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;

namespace Mercury.Snapshot.Commands
{
    public class GoogleSync
    {
        [Command(new string[] { "google-sync" }, "Connect your Google account to Mercury.")]
        public static async void Sync(CommandArguments Args)
        {
            string ToSend = Program.CurrentApp.Initializer.GoogleOAuth2.CreateAuthorizationRequest(new(Args.SlashCommand.User.Id.ToString()));
            await Args.SlashCommand.RespondAsync("", new Embed[]
            {
                new EmbedBuilder()
                {
                    Timestamp = DateTime.UtcNow,
                    Title = "Google Syncing",
                    Description = $"By [connecting your Google acccount]({ToSend}), you are letting me see events placed on your calendar, as well as enabling me to help you budget. :)",
                }.Build()
            }, false, true);
            Program.CurrentApp.Initializer.GoogleOAuth2.TokenPOSTed += async (UserCredential, TokResponse, OriginalCall) =>
            {
                if (OriginalCall.ApplicationAppliedTag == Args.SlashCommand.User.Id.ToString())
                {
                    await Args.SlashCommand.FollowupAsync("auth <3", null, false, true);
                }
            };
        }
    }
}
