using Mercury.Snapshot.Objects.Structures.Generics;
using Mercury.Snapshot.Objects.Structures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Mercury.Calendars
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
            throw new NotImplementedException();
        }
    }
}
