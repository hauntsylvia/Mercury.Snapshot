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
        [Command(new string[] { "snapshot" }, "Syncs calendar events and expenditures before sending a life summary.", Defer = true, LocalOnly = true)]
        public static async Task Snap(CommandArguments Args)
        {
            MercuryUser Profile = new(Args.SlashCommand.User.Id);
            await Profile.Calendar.Pull().ConfigureAwait(false);
            await Profile.ExpenditureLog.Pull().ConfigureAwait(false);
            await Args.SlashCommand.FollowupAsync("Here you go . .", new[] { new EmbedBuilder()
            {
                Color = new(0x00000),
                Footer = new()
                {
                    Text = "-☿-"
                },
                Description = "<a:loadinghearts:950503533910835241>"
            }.Build() }, false, true).ConfigureAwait(false);
            await Args.SlashCommand.ModifyOriginalResponseAsync(MessageInfo =>
            {
                MessageInfo.Embed = new SnapshotEmbed(Args, Profile).Build();
                MessageInfo.Content = "";
            }).ConfigureAwait(false);
        }
    }
}
