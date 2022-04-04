using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using izolabella.Discord.Internals.Structures.Commands;
using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.Embeds;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Commands
{
    internal class CalendarCommands
    {
        [Command(new string[] { "log-calendar-event" }, "Log a calendar event.", Defer = false, LocalOnly = true)]
        internal static async void LogCalendarEvent(CommandArguments Args, string Title, string Description, string StartsAt, string EndsAt)
        {
            string GeneralDate = "yyyy-MM-dd";
            string GeneralDateTime = "yyyy-MM-dd HH:mm:ss";
            MercuryUser User = new(Args.SlashCommand.User.Id);
            if (User.Calendar != null)
            {
                if((((DateTime.TryParseExact(StartsAt, new string[] { GeneralDate, GeneralDateTime }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out DateTime Start) || DateTime.TryParse(StartsAt, out Start)) &&
                    DateTime.TryParseExact(EndsAt, new string[] { GeneralDate, GeneralDateTime }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out DateTime End)) || DateTime.TryParse(EndsAt, out End)))
                {
                    MercuryCalendarEvent Event = new(Title, Description, DateTime.UtcNow, DateTime.UtcNow, Start, End, Origins.Mercury, Identifier.GetIdentifier());
                    await User.Calendar.SaveEvents(Event).ConfigureAwait(false);
                    await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Calendars.CalendarEventSuccessfullyLogged, new Embed[] { new CalendarEventLoggedEmbed(Event, User.Settings.CultureSettings.Culture).Build() }, false, true).ConfigureAwait(false);
                }
                else
                {
                    await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Calendars.CalendarEventFailureToLog, null, false, true).ConfigureAwait(false);
                }
            }
            else
            {
                await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Calendars.CalendarEventFailureToLog, null, false, true).ConfigureAwait(false);
            }
        }
        [Command(new string[] { "sync-calendars" }, "Sync all connected calendars.", Defer = false, LocalOnly = true)]
        internal static async Task CalendarSync(CommandArguments Args)
        {
            MercuryUser User = new(Args.SlashCommand.User.Id);
            if (User.Calendar != null)
            {
                await Args.SlashCommand.RespondAsync("", new Embed[] { new CalendarSyncEmbed(true, true).Build() }, false, true).ConfigureAwait(false);
                await User.Calendar.Pull().ConfigureAwait(false);
                await User.Calendar.Push().ConfigureAwait(false);
                await Args.SlashCommand.FollowupAsync("", new Embed[] { new CalendarSyncEmbed(false, true).Build() }, false, true).ConfigureAwait(false);
            }
            else
            {
                await Args.SlashCommand.FollowupAsync("", new Embed[] { new CalendarSyncEmbed(false, false).Build() }, false, true).ConfigureAwait(false);
            }
        }
    }
}
