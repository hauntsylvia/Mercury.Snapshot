using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using izolabella.OpenWeatherMap.NET;
using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Commands
{
    public static class Settings
    {
        [Command(new[] { "assign-language-settings" }, "Personalizes things such as decimal delimiters and dates.", Defer = false, LocalOnly = true)]
        public static async void ChangeSettings(CommandArguments Args, string ISOLanguageCode, UnitTypes Units, string TimezoneIdentifier = "U.S. Eastern Standard Time")
        {
            MercuryUser User = new(Args.SlashCommand.User.Id);
            User.Settings = new(User.Settings.GoogleCalendarSettings, User.Settings.GoogleSheetsSettings, User.Settings.WeatherSettings, new(ISOLanguageCode, Units, TimezoneIdentifier.ToString()));
            await Args.SlashCommand.RespondAsync(Strings.SettingsStrings.SettingsSaved, null, false, true).ConfigureAwait(false);
        }
    }
}
