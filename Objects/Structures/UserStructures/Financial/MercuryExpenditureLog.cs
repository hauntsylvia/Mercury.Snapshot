using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    public class MercuryExpenditureLog : IExpenditureLog, IMercuryExpenditureLog
    {
        public MercuryExpenditureLog(MercuryUser User)
        {
            this.User = User;
        }

        public MercuryUser User { get; }

        public Task<IReadOnlyCollection<ExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            if (this.User.ExpenditureEntriesRegister != null)
            {
                IReadOnlyCollection<Record<ExpenditureEntry>> AllExpenditures = this.User.ExpenditureEntriesRegister.GetAllRecords();
                return (Task<IReadOnlyCollection<ExpenditureEntry>>)AllExpenditures.OrderByDescending(Record => Record.UTCTimestamp).Where(Record => Record.UTCTimestamp >= TimeMin && Record.UTCTimestamp <= TimeMax).SkipLast(MaxResults);
            }
            else
            {
                return Task.FromResult<IReadOnlyCollection<ExpenditureEntry>>(new List<ExpenditureEntry>());
            }
        }

        public Task SaveExpenditures(params ExpenditureEntry[] Entries)
        {
            if (this.User.ExpenditureEntriesRegister != null)
            {
                foreach (ExpenditureEntry Entry in Entries)
                {
                    this.User.ExpenditureEntriesRegister.SaveRecord(Entry.Id, new Record<ExpenditureEntry>(Entry));
                }
            }
            return Task.CompletedTask;
        }
    }
}
