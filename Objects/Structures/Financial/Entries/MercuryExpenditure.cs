namespace Mercury.Snapshot.Objects.Structures.Financial.Entries
{
    public class MercuryExpenditure : IExpenditureEntry
    {
        public MercuryExpenditure(DateTime Timestamp, decimal DollarAmount, string PayeeOrPayer, string Category, Origins Origin)
        {
            this.Timestamp = Timestamp;
            this.DollarAmount = DollarAmount;
            this.PayeeOrPayer = PayeeOrPayer;
            this.Category = Category;
            this.Origin = Origin;
        }

        public DateTime Timestamp { get; }
        public decimal DollarAmount { get; }
        public string PayeeOrPayer { get; }
        public string Category { get; }
        public Origins Origin { get; }
    }
}
