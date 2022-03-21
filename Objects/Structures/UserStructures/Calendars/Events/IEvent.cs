using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events
{
    public interface IEvent : ISaveable
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
        Origins Origin { get; }
    }
}
