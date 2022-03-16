namespace Mercury.Snapshot.Objects.Structures.Financial
{
    public class Expenditure
    {
        public Expenditure(DateTime Timestamp, decimal DollarAmount, string PayeeOrPayer, string Category)
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
