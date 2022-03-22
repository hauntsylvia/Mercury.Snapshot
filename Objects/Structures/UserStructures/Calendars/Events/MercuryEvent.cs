using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Calendars.Events
{
    public class MercuryEvent : IEvent
    {
        public MercuryEvent(string? Summary, string? Description, DateTime? LastUpdated, DateTime? Created, DateTime? Start, DateTime? End, Origins? Origin, ulong? Id)
        {
            this.Summary = Summary ?? string.Empty;
            this.Description = Description ?? string.Empty;
            this.LastUpdated = LastUpdated ?? DateTime.UtcNow;
            this.Created = Created ?? DateTime.UtcNow;
            this.Start = Start ?? DateTime.UtcNow;
            this.End = End ?? DateTime.UtcNow;
            this.Origin = Origin ?? Origins.Unknown;
            this.Id = Id ?? Identifier.GetIdentifier();
        }

        public string Summary { get; }
        public string Description { get; }
        public DateTime LastUpdated { get; }
        public DateTime Created { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public Origins Origin { get; }
        public ulong Id { get; }
    }
}
