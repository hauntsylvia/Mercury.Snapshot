using Mercury.Snapshot.Objects.Structures.Calendars.Events;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.Calendars
{
    public class MercuryCalendar : ICalendar
    {
        public MercuryCalendar(MercuryProfile User)
        {
            this.User = User;
        }

        public MercuryProfile User { get; }
        public Task<IReadOnlyCollection<IEvent>> GetEvents(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            List<IEvent> Events = new();
            if(this.User.CalendarEventsRegister != null)
            {
                Events.AddRange(this.User.CalendarEventsRegister.GetAllRecords().Select(X => X.ObjectToStore));
            }
            return Task.FromResult<IReadOnlyCollection<IEvent>>(Events);
        }
    }
}
