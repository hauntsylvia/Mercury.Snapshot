using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Util;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization
{
    internal class MercuryUser
    {
        internal MercuryUser(ulong DiscordId, MercuryUserSettings Settings)
        {
            this.DiscordId = DiscordId;
            this.Settings = Settings;
            this.GoogleClient = new(this);
            this.CalendarEventsRegister = Registers.CalendarsRegister.GetSubRegister<ICalendar>(this.DiscordId)?.GetSubRegister<CalendarEvent>(this.DiscordId);
        }

        internal MercuryUser(ulong DiscordId)
        {
            this.DiscordId = DiscordId;
            this.GoogleClient = new(this);
            this.CalendarEventsRegister = Registers.CalendarsRegister.GetSubRegister<ICalendar>(this.DiscordId)?.GetSubRegister<CalendarEvent>(this.DiscordId);
        }

        internal GoogleClient GoogleClient { get; }

        internal MercuryUserSettings Settings
        {
            get => Registers.MercurySettingsRegister.GetRecord(this.DiscordId.ToString())?.ObjectToStore ?? new();
            set => Registers.MercurySettingsRegister.SaveRecord(this.DiscordId.ToString(), new Record<MercuryUserSettings>(value));
        }
        
        internal async Task<IReadOnlyCollection<CalendarEvent>> GetAllCalendarEventsAsync(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            List<CalendarEvent> Events = new();
            foreach (ICalendar? Calendar in this.Calendars)
            {
                if (Calendar != null)
                {
                    IReadOnlyCollection<CalendarEvent> CalendarEvents = await Calendar.GetEvents(TimeMin, TimeMax, MaxResults).ConfigureAwait(false);
                    foreach (CalendarEvent Event in CalendarEvents)
                    {
                        CalendarEvent? AlreadyHere = Events.FirstOrDefault(FromList =>
                        {
                            return FromList.Start == Event.Start && FromList.Summary == Event.Summary && FromList.Description == Event.Description;
                        });
                        if (AlreadyHere == null)
                        {
                            Events.Add(Event);
                        }
                    }
                }
            }
            return Events;
        }
        internal IReadOnlyCollection<ICalendar?> Calendars
        {
            get => new List<ICalendar?>()
            {
                this.GoogleClient.CalendarManager,
                this.Calendar
            };
        }
        internal MercuryCalendar Calendar
        {
            get => Registers.CalendarsRegister.GetSubRegister<MercuryCalendar>(this.DiscordId)?.GetRecord("primary")?.ObjectToStore ?? new MercuryCalendar(this);
            set => Registers.CalendarsRegister.GetSubRegister<MercuryCalendar>(this.DiscordId)?.SaveRecord("primary", new Record<MercuryCalendar>(value));
        }

        internal IReadOnlyCollection<IExpenditureLog?> ExpenditureLogs
        {
            get => new List<IExpenditureLog?>()
            {
                this.ExpenditureLog,
                this.GoogleClient.IsAuthenticated ? this.GoogleClient.SheetsManager : null
            };
        }
        internal MercuryExpenditureLog ExpenditureLog
        {
            get => Registers.ExpenditureLogsRegister.GetSubRegister<MercuryExpenditureLog>(this.DiscordId)?.GetRecord("primary")?.ObjectToStore ?? new MercuryExpenditureLog(this);
            set => Registers.ExpenditureLogsRegister.GetSubRegister<MercuryExpenditureLog>(this.DiscordId)?.SaveRecord("primary", new Record<MercuryExpenditureLog>(value));
        }

        internal Register<CalendarEvent>? CalendarEventsRegister { get; }
        internal Register<ExpenditureEntry>? ExpenditureEntriesRegister { get; }

        internal ulong DiscordId { get; }
    }
}
