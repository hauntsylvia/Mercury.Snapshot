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
                Events.AddRange(this.User.CalendarEventsRegister.GetAllRecords().Select(X => X.ObjectToStore));
            }
            return Task.FromResult<IReadOnlyCollection<IEvent>>(Events);
        }

        public Task SaveEvents(params IEvent[] Events)
        {
            if (this.User.CalendarEventsRegister != null)
            {
                foreach (IEvent Event in Events)
                {
                    this.User.CalendarEventsRegister.SaveRecord(Event.Id, new Record<IEvent>(Event));
                }
            }
            return Task.CompletedTask;
        }
    }
}
