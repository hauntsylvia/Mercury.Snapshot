namespace Mercury.Snapshot.Objects.Structures.Financial.Entries
{
    public interface IExpenditure
    {
        public DateTime Timestamp { get; }
        public decimal DollarAmount { get; }
        public string PayeeOrPayer { get; }
        public string Category { get; }
        public Origins Origin { get; }
    }
}
