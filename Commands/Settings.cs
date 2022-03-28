using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Commands
{
    public enum Test
    {
        ABC = 0,
        BBB = 2
    }
    public static class Settings
    {
        [Command(new[] { "assign-settings" }, "Tells me what settings to use for personalized responses.", Defer = false, LocalOnly = true)]
        public static async void ChangeSettings(CommandArguments Args, string ExpenditureSheetId, string ZipCode, Test AAA)
        {
            MercuryUser User = new(Args.SlashCommand.User.Id);
            User.Settings = new(new("primary"), new(ExpenditureSheetId), new(ZipCode), new());
            await Args.SlashCommand.RespondAsync(Strings.SettingsStrings.SettingsSaved, null, false, true).ConfigureAwait(false);
        }
    }
}
