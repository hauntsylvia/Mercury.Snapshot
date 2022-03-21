using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    public interface IExpenditureLog
    {
        public Task<IReadOnlyCollection<IExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults);
        public Task SaveExpenditures(params IExpenditureEntry[] Entries);
    }
}
