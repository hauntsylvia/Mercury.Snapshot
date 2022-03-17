using Mercury.Snapshot.Objects.Structures.Financial.Entries;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.Financial
{
    public interface IExpenditureLog
    {
        public IRegister<IExpenditure> OwnersExpenditureLog { get; }
        public Task<IReadOnlyCollection<IExpenditure>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults);
    }
}
