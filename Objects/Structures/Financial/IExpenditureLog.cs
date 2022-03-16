using Mercury.Snapshot.Objects.Structures.Financial.Entries;
using Mercury.Unification.IO.File.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Financial
{
    public interface IExpenditureLog
    {
        public IRegister<IExpenditure> OwnersExpenditureLog { get; }
        public Task<IReadOnlyCollection<IExpenditure>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults);
    }
}
