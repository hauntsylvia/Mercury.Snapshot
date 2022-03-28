using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events
{
    public class CalendarEvent : ISaveable
    {
        public CalendarEvent(string Summary, string Description, DateTime LastUpdated, DateTime Created, DateTime Start, DateTime End, Origins Origin, ulong Id)
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

        public string Summary { get; }
        public string Description { get; }
        public DateTime LastUpdated { get; }
        public DateTime Created { get; }
        public DateTime Start { get; }
        public DateTime End { get; }

        /// <summary>
        /// From where this event originates.
        /// </summary>
        public Origins Origin { get; }

        public ulong Id { get; }
    }
}
