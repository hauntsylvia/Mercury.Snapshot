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
    public class Settings
    {
        [Command(new[] { "assign-settings" }, "Tells me what settings to use for personalized responses.", Defer = false, LocalOnly = true)]
        public static async void ChangeSettings(CommandArguments Args, string ExpenditureSheetId, string ZipCode)
        {
            MercuryUser User = new(Args.SlashCommand.User.Id);
            User.Settings = new Unification.IO.File.Records.Record<MercuryUserSettings>(new(new("primary"), new(ExpenditureSheetId), new(ZipCode)));
            await Args.SlashCommand.RespondAsync(Strings.SettingsStrings.SettingsSaved, null, false, true);
        }
    }
}
