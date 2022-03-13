using Mercury.Snapshot.Objects.Structures.Generics;
using Mercury.Snapshot.Objects.Structures.Personalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
