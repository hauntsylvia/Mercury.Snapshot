using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Financial
{
    internal class Expenditure
    {
        internal Expenditure(DateTime Timestamp, decimal DollarAmount, string PayeeOrPayer, string Category)
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
