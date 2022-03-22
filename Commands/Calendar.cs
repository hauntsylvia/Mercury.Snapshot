using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using izolabella.Discord.Internals.Structures.Commands;
using Mercury.Snapshot.Objects.Structures.Embeds;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Commands
{
    public class Calendar
    {
        [Command(new string[] { "log-calendar-event" }, "Log a calendar event.", Defer = false, LocalOnly = true)]
        public static async void LogCalendarEvent(CommandArguments Args, string Title, string Description, string StartsAt, string EndsAt)
        {
            string GeneralDate = "yyyy-MM-dd";
            string GeneralDateTime = "yyyy-MM-dd HH:mm:ss";
            MercuryUser User = new(Args.SlashCommand.User.Id);
            if (User.Calendar != null)
            {
                if((((DateTime.TryParseExact(StartsAt, new string[] { GeneralDate, GeneralDateTime }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out DateTime Start) || DateTime.TryParse(StartsAt, out Start)) &&
                    DateTime.TryParseExact(EndsAt, new string[] { GeneralDate, GeneralDateTime }, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out DateTime End)) || DateTime.TryParse(EndsAt, out End)))
                {
                    MercuryEvent Event = new(Title, Description, DateTime.UtcNow, DateTime.UtcNow, Start, End, Origins.Mercury, Identifier.GetIdentifier());
                    await User.Calendar.SaveEvents(Event);
                    await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Calendars.CalendarEventSuccessfullyLogged, new Embed[] { new CalendarEventLoggedEmbed(Event).Build() }, false, true);
                }
                else
                {
                    await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Calendars.CalendarEventFailureToLog, null, false, true);
                }
            }
            else
            {
                await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Calendars.CalendarEventFailureToLog, null, false, true);
            }
        }
        [Command(new string[] { "sync-calendars" }, "Sync all connected calendars.", Defer = false, LocalOnly = true)]
        public static async void CalendarSync(CommandArguments Args)
        {
            MercuryUser User = new(Args.SlashCommand.User.Id);
            if (User.CalendarEventsRegister != null && User.Calendar != null && User.GoogleClient.CalendarManager != null)
            {
                if (User.Calendar != null)
                {
                    await Args.SlashCommand.RespondAsync("", new Embed[] { new CalendarSyncEmbed().Build() }, false, true);
                    await User.Calendar.BufferPull(User.GoogleClient.CalendarManager);
                    await Args.SlashCommand.FollowupAsync(Strings.EmbedStrings.Calendars.CalendarSyncSuccess, null, false, true);
                }
                else
                {
                    await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Calendars.CalendarSyncFailure, null, false, true);
                }
            }
            else
            {
                await Args.SlashCommand.RespondAsync(Strings.EmbedStrings.Calendars.CalendarSyncFailure, null, false, true);
            }
        }
    }
}
