using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries
{
    internal class ExpenditureEntry : ISaveable
    {
        internal ExpenditureEntry(DateTime Timestamp, double DollarAmount, string PayeeOrPayer, string Category, Origins Origin, ulong Id)
        {
            this.Timestamp = Timestamp;
            this.DollarAmount = DollarAmount;
            this.PayeeOrPayer = PayeeOrPayer;
            this.Category = Category;
            this.Origin = Origin;
            this.Id = Id;
        }

        internal DateTime Timestamp { get; }
        internal double DollarAmount { get; }
        internal string PayeeOrPayer { get; }
        internal string Category { get; }
        internal Origins Origin { get; }
        internal ulong Id { get; }
    }
}
