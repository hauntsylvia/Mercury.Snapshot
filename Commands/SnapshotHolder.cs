using izolabella.ConsoleHelper;
using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Structures.Embeds;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;

namespace Mercury.Snapshot.Commands
{
    public static class SnapshotHolder
    {
        [Command(new string[] { "snapshot" }, "Receive a general rundown.", Defer = true, LocalOnly = true)]
        public static async Task Snap(CommandArguments Args)
        {
            try
            {
                MercuryUser Profile = new(Args.SlashCommand.User.Id);
                await Args.SlashCommand.FollowupAsync("Here you go . .", new[] { new EmbedBuilder()
                {
                    Color = new(0x00000),
                    Footer = new()
                    {
                        Text = "-☿-"
                    },
                    Description = "<a:loadinghearts:950503533910835241>"
                }.Build() }, false, true);
                await Args.SlashCommand.ModifyOriginalResponseAsync(MessageInfo =>
                {
                    MessageInfo.Embed = new SnapshotEmbed(Args, Profile).Build();
                    MessageInfo.Content = "";
                });
            }
            catch (Exception E)
            {
                PrettyConsole.Log("Snapshot Error", E, LoggingLevel.Errors);
            }
        }
    }
}
