using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars;
using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Util;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Personalization
{
    public class MercuryUser
    {
        public MercuryUser(ulong DiscordId, MercuryUserSettings Settings)
        {
            this.DiscordId = DiscordId;
            this.Settings = new Record<MercuryUserSettings>(Settings, new List<string>());
            this.GoogleClient = new(this.DiscordId);
            this.CalendarRegister = new Register<ICalendar>(this.DiscordId);
            this.ExpenditureRegister = new Register<IExpenditureLog>(this.DiscordId);
        }

        public MercuryUser(ulong DiscordId)
        {
            this.DiscordId = DiscordId;
            this.GoogleClient = new(this.DiscordId);
            this.CalendarRegister = Registers.CalendarsRegister.GetSubRegister<ICalendar>(this.DiscordId);
            this.ExpenditureRegister = Registers.ExpenditureLogsRegister.GetSubRegister<IExpenditureLog>(this.DiscordId);
        }

        public GoogleClient GoogleClient { get; }

        public IRecord<MercuryUserSettings> Settings
        {
            get => Registers.MercurySettingsRegister.GetRecord(this.DiscordId.ToString()) ?? new Record<MercuryUserSettings>(new(), new List<string>() { "Auto-generated" });
            set => Registers.MercurySettingsRegister.SaveRecord(this.DiscordId.ToString(), value);
        }

        public Register<ICalendar>? CalendarRegister { get; }
        public Register<IExpenditureLog>? ExpenditureRegister { get; }

        public Register<IEvent>? CalendarEventsRegister => this.CalendarRegister?.GetSubRegister<IEvent>(this.DiscordId);
        public Register<IExpenditureEntry>? ExpenditureEntriesRegister => this.ExpenditureRegister?.GetSubRegister<IExpenditureEntry>(this.DiscordId);

        public ulong DiscordId { get; }
    }
}
