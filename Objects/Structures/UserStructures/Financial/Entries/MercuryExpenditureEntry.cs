using Mercury.Snapshot.Consts.Enums;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries
{
    public class MercuryExpenditureEntry : ExpenditureEntry
    {
        public MercuryExpenditureEntry(DateTime Timestamp, double DollarAmount, string PayeeOrPayer, string Category, Origins Origin, ulong Id) : base(Timestamp, DollarAmount, PayeeOrPayer, Category, Origin, Id)
        {
        }
    }
}
