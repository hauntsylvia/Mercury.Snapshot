using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using Mercury.Snapshot.Objects.Structures.Embeds;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;

namespace Mercury.Snapshot.Commands
{
    public class GoogleConnect
    {
        [Command(new string[] { "google-connect" }, "Connect your Google account to Mercury.", Defer = false, LocalOnly = true)]
        public static async void Sync(CommandArguments Args)
        {
            MercuryUser Profile = new(Args.SlashCommand.User.Id);
            string ToSend = Program.CurrentApp.Initializer.GoogleOAuth2.CreateAuthorizationRequest(new(Args.SlashCommand.User.Id.ToString()));
            await Args.SlashCommand.RespondAsync("", new Embed[]
            {
                new GoogleAuthPrompt(ToSend).Build(),
            }, false, true);
            Program.CurrentApp.Initializer.GoogleOAuth2.TokenPOSTed += async (UserCredential, TokResponse, OriginalCall) =>
            {
                if (OriginalCall.ApplicationAppliedTag == Args.SlashCommand.User.Id.ToString())
                {
                    await Args.SlashCommand.FollowupAsync("Successful authorization. <3 - You can close that window now (or leave it open, I judge silently because I hate confrontation).", null, false, true);
                }
            };
        }
    }
}
