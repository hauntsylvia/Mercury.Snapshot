using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events
{
    internal class CalendarEvent : ISaveable
    {
        internal CalendarEvent(string Summary, string Description, DateTime LastUpdated, DateTime Created, DateTime Start, DateTime End, Origins Origin, ulong Id)
        {
            this.Summary = Summary;
            this.Description = Description;
            this.LastUpdated = LastUpdated;
            this.Created = Created;
            this.Start = Start;
            this.End = End;
            this.Origin = Origin;
            this.Id = Id;
        }

        internal string Summary { get; }
        internal string Description { get; }
        internal DateTime LastUpdated { get; }
        internal DateTime Created { get; }
        internal DateTime Start { get; }
        internal DateTime End { get; }

        /// <summary>
        /// From where this event originates.
        /// </summary>
        internal Origins Origin { get; }

        internal ulong Id { get; }
    }
}
