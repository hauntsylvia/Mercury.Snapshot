using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    public class MercuryExpenditureLog : IExpenditureLog, IMercuryExpenditureLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OwnerId">Owner's id on Discord.</param>
        public MercuryExpenditureLog(MercuryUser User)
        {
            this.User = User;
        }

        public MercuryUser User { get; }

        public Task<IReadOnlyCollection<IExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            if (this.User.ExpenditureEntriesRegister != null)
            {
                IReadOnlyCollection<IRecord<IExpenditureEntry>> AllExpenditures = this.User.ExpenditureEntriesRegister.GetAllRecords();
                return (Task<IReadOnlyCollection<IExpenditureEntry>>)AllExpenditures.OrderByDescending(Record => Record.UTCTimestamp).Where(Record => Record.UTCTimestamp >= TimeMin && Record.UTCTimestamp <= TimeMax).SkipLast(MaxResults);
            }
            else
            {
                return Task.FromResult<IReadOnlyCollection<IExpenditureEntry>>(new List<IExpenditureEntry>());
            }
        }

        public Task SaveExpenditures(params IExpenditureEntry[] Entries)
        {
            if (this.User.ExpenditureEntriesRegister != null)
            {
                foreach (IExpenditureEntry Entry in Entries)
                {
                    this.User.ExpenditureEntriesRegister.SaveRecord(Entry.Id, new Record<IExpenditureEntry>(Entry));
                }
            }
            return Task.CompletedTask;
        }
    }
}
