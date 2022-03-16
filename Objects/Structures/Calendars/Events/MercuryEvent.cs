namespace Mercury.Snapshot.Objects.Structures.Calendars.Events
{
    public class MercuryEvent : IEvent
    {
        public MercuryEvent(string Summary, string Description, DateTime LastUpdated, DateTime Created, DateTime Start, DateTime End, string Origin)
        {
            this.Summary = Summary;
            this.Description = Description;
            this.LastUpdated = LastUpdated;
            this.Created = Created;
            this.Start = Start;
            this.End = End;
            this.Origin = Origin;
        }

        public string Summary { get; }
        public string Description { get; }
        public DateTime LastUpdated { get; }
        public DateTime Created { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public string Origin { get; }
    }
}
