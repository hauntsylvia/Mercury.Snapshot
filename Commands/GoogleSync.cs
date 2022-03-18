using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using Mercury.Snapshot.Objects.Structures.Embeds;
using Mercury.Snapshot.Objects.Structures.Personalization;

namespace Mercury.Snapshot.Commands
{
    public class GoogleSync
    {
        [Command(new string[] { "google-sync" }, "Connect your Google account to Mercury.")]
        public static async void Sync(CommandArguments Args, bool? ReceiveSnapshotOnCompletion)
        {
            MercuryProfile Profile = new(Args.SlashCommand.User.Id);
            string ToSend = Program.CurrentApp.Initializer.GoogleOAuth2.CreateAuthorizationRequest(new(Args.SlashCommand.User.Id.ToString()));
            await Args.SlashCommand.RespondAsync("", new Embed[]
            {
                new GoogleAuthPrompt(ToSend).Build(),
            }, false, true);
            Program.CurrentApp.Initializer.GoogleOAuth2.TokenPOSTed += async (UserCredential, TokResponse, OriginalCall) =>
            {
                if (OriginalCall.ApplicationAppliedTag == Args.SlashCommand.User.Id.ToString())
                {
                    if(ReceiveSnapshotOnCompletion.HasValue && ReceiveSnapshotOnCompletion.Value)
                    {
                        await Args.SlashCommand.FollowupAsync("", new Embed[] { new SnapshotEmbed(Args, Profile).Build() }, false, true);
                    }
                    else
                    {
                        await Args.SlashCommand.FollowupAsync("Successful authorization. <3", null, false, true);
                    }
                }
            };
        }
    }
}
