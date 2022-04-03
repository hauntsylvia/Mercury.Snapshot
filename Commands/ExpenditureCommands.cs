using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using izolabella.Discord.Internals.Structures.Commands;
using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.Embeds;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Commands
{
    public static class ExpenditureCommands
    {
        [Command(new string[] { "log-expenditure" }, "Log an expenditure.", Defer = false, LocalOnly = true)]
        public static async void LogExpenditure(CommandArguments Args, double Amount, string PayerOrPayee, string Category, bool Credit = false)
        {
            MercuryUser User = new(Args.SlashCommand.User.Id);
            if (User.ExpenditureEntriesRegister != null)
            {
                MercuryExpenditureEntry Entry = new(DateTime.UtcNow, Credit ? (Math.Abs(Amount)) : (-Math.Abs(Amount)), PayerOrPayee, Category ?? string.Empty, Origins.Mercury, Identifier.GetIdentifier());
                await User.ExpenditureLog.SaveExpenditures(Entry).ConfigureAwait(false);
                await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Expenditures.ExpenditureSuccessfullyLogged, new Embed[] { new ExpenditureLoggedEmbed(Entry, User.Settings.CultureSettings.Culture).Build() }, false, true).ConfigureAwait(false);
            }
            else
            {
                await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Expenditures.ExpenditureLogFailed, null, false, true).ConfigureAwait(false);
            }
        }
        [Command(new string[] { "sync-expenditurelogs" }, "Sync all expenditure logs.", Defer = false, LocalOnly = true)]
        public static async void ExpenditureLogSync(CommandArguments Args)
        {
            MercuryUser User = new(Args.SlashCommand.User.Id);
            if (User.ExpenditureLog != null)
            {
                await Args.SlashCommand.RespondAsync("", new Embed[] { new ExpenditureSyncEmbed(true, true).Build() }, false, true).ConfigureAwait(false);
                await User.ExpenditureLog.Pull().ConfigureAwait(false);
                await User.ExpenditureLog.Push().ConfigureAwait(false);
                await Args.SlashCommand.FollowupAsync("", new Embed[] { new ExpenditureSyncEmbed(false, true).Build() }, false, true).ConfigureAwait(false);
            }
            else
            {
                await Args.SlashCommand.RespondAsync("", new Embed[] { new ExpenditureSyncEmbed(false, false).Build() }, false, true).ConfigureAwait(false);
            }
        }
    }
}
