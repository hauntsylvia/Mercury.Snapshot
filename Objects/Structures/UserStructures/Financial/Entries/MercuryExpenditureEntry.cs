namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries
{
    public class MercuryExpenditureEntry : IExpenditureEntry
    {
        public MercuryExpenditureEntry(DateTime Timestamp, double DollarAmount, string PayeeOrPayer, string Category, Origins Origin, ulong Id)
        {
            this.Timestamp = Timestamp;
            this.DollarAmount = DollarAmount;
            this.PayeeOrPayer = PayeeOrPayer;
            this.Category = Category;
            this.Origin = Origin;
            this.Id = Id;
        }

        public DateTime Timestamp { get; }
        public double DollarAmount { get; }
        public string PayeeOrPayer { get; }
        public string Category { get; }
        public Origins Origin { get; }
        public ulong Id { get; }
    }
}
