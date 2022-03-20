using Mercury.Snapshot.Objects.Structures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Structures.Financial
{
    public class MercuryExpenditureLog : IExpenditureLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OwnerId">Owner's id on Discord.</param>
        public MercuryExpenditureLog(MercuryProfile User)
        {
            this.User = User;
        }

        public MercuryProfile User { get; }
        public Task<IReadOnlyCollection<IExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            if(this.User.ExpenditureEntriesRegister != null)
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
                    if(Entry != null)
                    {
                        this.User.ExpenditureEntriesRegister.SaveRecord()
                    }
                }
            }
        }
    }
}
