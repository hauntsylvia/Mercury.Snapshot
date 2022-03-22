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
        public Task<IReadOnlyCollection<IEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            List<IEvent> Events = new();
            if (this.User.CalendarEventsRegister != null)
            {
                Events.AddRange(this.User.CalendarEventsRegister.GetAllRecords().Select(X => X.ObjectToStore).Where(Z => Z.Start >= TimeMin && Z.End <= TimeMax));
            }
            return Task.FromResult<IReadOnlyCollection<IEvent>>(Events.SkipLast(Events.Count - MaxResults).ToList());
        }

        public Task SaveEvents(params IEvent[] Events)
        {
            if (this.User.CalendarEventsRegister != null)
            {
                foreach (MercuryEvent Event in Events)
                {
                    this.User.CalendarEventsRegister.SaveRecord(Event.Id, new Record<MercuryEvent>(Event));
                }
            }
            return Task.CompletedTask;
        }
        
        private async Task Pull(DateTime Min, DateTime Max, ICalendar?[] CalendarsToSync, int BufferSize)
        {
            IEvent[] MercuryEvents = (await this.GetEvents(Min, Max, BufferSize)).ToArray();
            foreach (ICalendar? Calendar in CalendarsToSync)
            {
                if (Calendar != null)
                {
                    IEvent[] Events = (await Calendar.GetEvents(Min, Max, BufferSize)).ToArray();
                    for (int EventIndex = 0; EventIndex < BufferSize; EventIndex++)
                    {
                        if(Events.Length > EventIndex)
                        {
                            IEvent Event = Events.ElementAt(EventIndex);
                            IEvent? MercuryEvent = MercuryEvents.FirstOrDefault(Ev =>
                            {
                                return Ev.Start == Event.Start && Ev.Summary == Event.Summary && Ev.Description == Event.Description;
                            });
                            if (Calendar.GetType() != typeof(MercuryCalendar))
                            {
                                if (MercuryEvent == null)
                                {
                                    if (Event.Start > Min)
                                    {
                                        Min = Event.Start;
                                    }
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

        public async Task BufferPull(params ICalendar?[] CalendarsToSync)
        {
            await this.Pull(DateTime.MinValue, DateTime.MaxValue, CalendarsToSync, 128);
        }

        Task ICalendar.SaveEvents(params IEvent[] Events)
        {
            throw new NotImplementedException();
        }
    }
}
