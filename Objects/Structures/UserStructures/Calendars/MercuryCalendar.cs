using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars
{
    public class MercuryCalendar : ICalendar, IMercuryCalendar
    {
        public MercuryCalendar(MercuryUser User)
        {
            this.User = User;
        }

        public MercuryUser User { get; }
        public Task<IReadOnlyCollection<CalendarEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            List<CalendarEvent> Events = new();
            if (this.User.CalendarEventsRegister != null)
            {
                Events.AddRange(this.User.CalendarEventsRegister.GetAllRecords().Select(X => X.ObjectToStore).Where(Z => Z.Start >= TimeMin && Z.End <= TimeMax));
            }
            return Task.FromResult<IReadOnlyCollection<CalendarEvent>>(Events.SkipLast(Events.Count - MaxResults).ToList());
        }

        public Task SaveEvents(params CalendarEvent[] Events)
        {
            if (this.User.CalendarEventsRegister != null)
            {
                foreach (MercuryCalendarEvent Event in Events)
                {
                    this.User.CalendarEventsRegister.SaveRecord(Event.Id, new Record<CalendarEvent>(Event));
                }
            }
            return Task.CompletedTask;
        }
        
        private async Task Pull(DateTime Min, DateTime Max, ICalendar?[] CalendarsToSync, int BufferSize)
        {
            CalendarEvent[] MercuryEvents = (await this.GetEvents(Min, Max, BufferSize)).ToArray();
            foreach (ICalendar? Calendar in CalendarsToSync)
            {
                if (Calendar != null)
                {
                    CalendarEvent[] Events = (await Calendar.GetEvents(Min, Max, BufferSize)).ToArray();
                    for (int EventIndex = 0; EventIndex < BufferSize; EventIndex++)
                    {
                        if(Events.Length > EventIndex)
                        {
                            CalendarEvent Event = Events.ElementAt(EventIndex);
                            CalendarEvent? MercuryEvent = MercuryEvents.FirstOrDefault(Ev =>
                            {
                                return Ev.Start == Event.Start && Ev.Summary == Event.Summary && Ev.Description == Event.Description;
                            });
                            if (Calendar.GetType() != typeof(MercuryCalendar))
                            {
                                if (Event.Start > Min)
                                {
                                    Min = Event.Start;
                                }
                                if (MercuryEvent == null)
                                {
                                    await this.SaveEvents(Event);
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            await this.Pull(Min, Max, CalendarsToSync, BufferSize);
        }

        /// <summary>
        /// Pulls using a maximum value to download at a time from each calendar to prevent heavy memory use. This calls every
        /// calendar's <see cref="ICalendar.GetEvents(DateTime, DateTime, int)"/> method recursively until all events have been 
        /// retrieved. This method will also download every event by calling <see cref="ICalendar.SaveEvents(CalendarEvent[])"/> for each
        /// iteration.
        /// </summary>
        /// <param name="CalendarsToSync">The calendars to sync.</param>
        /// <returns></returns>
        public async Task BufferPull(params ICalendar?[] CalendarsToSync)
        {
            await this.Pull(DateTime.MinValue, DateTime.MaxValue, CalendarsToSync, 128);
        }
    }
}
