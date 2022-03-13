using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Generics
{
    public interface IEvent
    {
        string Summary { get; }
        string Description { get; }
        DateTime LastUpdated { get; }
        DateTime Created { get; }
        DateTime Start { get; }
        DateTime End { get; }

        /// <summary>
        /// From where this event originates.
        /// </summary>
        string Origin { get; }
    }
}
