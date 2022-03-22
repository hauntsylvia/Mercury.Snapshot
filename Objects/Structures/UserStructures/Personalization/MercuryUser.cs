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
            this.CalendarEventsRegister = Registers.CalendarsRegister.GetSubRegister<ICalendar>(this.DiscordId)?.GetSubRegister<MercuryEvent>(this.DiscordId);
            this.ExpenditureRegister = Registers.ExpenditureLogsRegister.GetSubRegister<IExpenditureLog>(this.DiscordId);
        }

        public MercuryUser(ulong DiscordId)
        {
            this.DiscordId = DiscordId;
            this.GoogleClient = new(this.DiscordId);
            this.CalendarEventsRegister = Registers.CalendarsRegister.GetSubRegister<ICalendar>(this.DiscordId)?.GetSubRegister<MercuryEvent>(this.DiscordId);
            this.ExpenditureRegister = Registers.ExpenditureLogsRegister.GetSubRegister<IExpenditureLog>(this.DiscordId);
        }

        public GoogleClient GoogleClient { get; }

        public Record<MercuryUserSettings> Settings
        {
            get => Registers.MercurySettingsRegister.GetRecord(this.DiscordId.ToString()) ?? new Record<MercuryUserSettings>(new(), new List<string>() { "Auto-generated" });
            set => Registers.MercurySettingsRegister.SaveRecord(this.DiscordId.ToString(), value);
        }

        public async Task<IReadOnlyCollection<IEvent>> GetAllCalendarEventsAsync(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            List<IEvent> Events = new();
            foreach(ICalendar? Calendar in this.Calendars)
            {
                if(Calendar != null)
                {
                    IReadOnlyCollection<IEvent> CalendarEvents = await Calendar.GetEvents(TimeMin, TimeMax, MaxResults);
                    foreach(IEvent Event in CalendarEvents)
                    {
                        IEvent? AlreadyHere = Events.FirstOrDefault(FromList =>
                        {
                            return FromList.Start == Event.Start && FromList.Summary == Event.Summary && FromList.Description == Event.Description;
                        });
                        if(AlreadyHere == null)
                        {
                            Events.Add(Event);
                        }
                    }
                }
            }
            return Events;
        }

        public IReadOnlyCollection<ICalendar?> Calendars
        {
            get
            {
                return new List<ICalendar?>()
                {
                    {
                        this.GoogleClient.CalendarManager
                    },
                    {
                        this.Calendar
                    }
                };
            }
        }
        public MercuryCalendar Calendar
        {
            get => Registers.CalendarsRegister.GetSubRegister<MercuryCalendar>(this.DiscordId)?.GetRecord("primary")?.ObjectToStore ?? new MercuryCalendar(this);
            set => Registers.CalendarsRegister.GetSubRegister<MercuryCalendar>(this.DiscordId)?.SaveRecord("primary", new Record<MercuryCalendar>(value));
        }
        public Register<MercuryEvent>? CalendarEventsRegister { get; }
        public Register<IExpenditureLog>? ExpenditureRegister { get; }
        public Register<IExpenditureEntry>? ExpenditureEntriesRegister => this.ExpenditureRegister?.GetSubRegister<IExpenditureEntry>(this.DiscordId);

        public ulong DiscordId { get; }
    }
}
