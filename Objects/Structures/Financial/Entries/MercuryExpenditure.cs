namespace Mercury.Snapshot.Objects.Structures.Financial.Entries
{
    public class MercuryExpenditure : IExpenditure
    {
        public MercuryExpenditure(DateTime Timestamp, decimal DollarAmount, string PayeeOrPayer, string Category)
        {
            this.Timestamp = Timestamp;
            this.DollarAmount = DollarAmount;
            this.PayeeOrPayer = PayeeOrPayer;
            this.Category = Category;
        }

        public DateTime Timestamp { get; }
        public decimal DollarAmount { get; }
        public string PayeeOrPayer { get; }
        public string Category { get; }
    }
}
