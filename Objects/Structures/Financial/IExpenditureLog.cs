using Mercury.Snapshot.Objects.Structures.Financial.Entries;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.Financial
{
    public interface IExpenditureLog
    {
        public IRegister<IExpenditureEntry> OwnersExpenditureLog { get; }
        public Task<IReadOnlyCollection<IExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults);
        public Task SaveExpenditures(params IExpenditureEntry[] Entries);
    }
}
