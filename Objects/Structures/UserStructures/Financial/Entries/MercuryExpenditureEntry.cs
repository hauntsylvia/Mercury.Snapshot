namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries
{
    internal class MercuryExpenditureEntry : ExpenditureEntry
    {
        internal MercuryExpenditureEntry(DateTime Timestamp, double DollarAmount, string PayeeOrPayer, string Category, Origins Origin, ulong Id) : base(Timestamp, DollarAmount, PayeeOrPayer, Category, Origin, Id)
        {
        }
    }
}
