using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    internal interface IExpenditureLog
    {
        internal Task<IReadOnlyCollection<ExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults);
        internal Task SaveExpenditures(params ExpenditureEntry[] Entries);
    }
}
