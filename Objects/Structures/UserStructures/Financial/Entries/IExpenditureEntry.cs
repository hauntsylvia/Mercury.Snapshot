using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries
{
    public interface IExpenditureEntry : ISaveable
    {
        public DateTime Timestamp { get; }
        public double DollarAmount { get; }
        public string PayeeOrPayer { get; }
        public string Category { get; }
        public Origins Origin { get; }
    }
}
