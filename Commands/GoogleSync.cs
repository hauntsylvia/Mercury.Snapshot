using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using Mercury.Snapshot.Objects.Structures.Embeds;

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
                new GoogleAuthPrompt(ToSend).Build(),
            }, false, true);
            Program.CurrentApp.Initializer.GoogleOAuth2.TokenPOSTed += async (UserCredential, TokResponse, OriginalCall) =>
            {
                if (OriginalCall.ApplicationAppliedTag == Args.SlashCommand.User.Id.ToString())
                {
                    await Args.SlashCommand.FollowupAsync("Successful authorization. <3", null, false, true);
                }
            };
        }
    }
}
