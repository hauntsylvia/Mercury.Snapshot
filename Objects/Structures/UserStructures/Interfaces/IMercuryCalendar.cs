using Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces
{
    public interface IMercuryCalendar
    {
        public Task SaveEvents(params IEvent[] Events);
    }
}
