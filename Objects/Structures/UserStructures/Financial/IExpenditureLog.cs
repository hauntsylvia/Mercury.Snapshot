using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    public interface IExpenditureLog
    {
        public Task<IReadOnlyCollection<ExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults);
        public Task SaveExpenditures(params ExpenditureEntry[] Entries);
    }
}
