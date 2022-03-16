namespace Mercury.Snapshot.Objects.Structures.Calendars.Events
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
        Origins Origin { get; }
    }
}
