using Mercury.Snapshot.Objects.Structures.Financial.Entries;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Financial
{
    public class MercuryExpenditureLog : IExpenditureLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OwnerId">Owner's id on Discord.</param>
        public MercuryExpenditureLog(ulong OwnerId)
        {
            this.OwnersExpenditureLog = Registers.ExpenditureLogsRegister.CreateSubRegister<IExpenditure>(OwnerId.ToString());
        }

        public IRegister<IExpenditure> OwnersExpenditureLog { get; }

        public Task<IReadOnlyCollection<IExpenditure>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            IReadOnlyCollection<IRecord<IExpenditure>> AllExpenditures = this.OwnersExpenditureLog.GetAllRecords();
            return (Task<IReadOnlyCollection<IExpenditure>>)AllExpenditures.OrderByDescending(Record => Record.UTCTimestamp).Where(Record => Record.UTCTimestamp >= TimeMin && Record.UTCTimestamp <= TimeMax).SkipLast(MaxResults);
        }

        public Task SaveExpenditures(IExpenditure Expenditure)
        {
            this.OwnersExpenditureLog.SaveRecord(Expenditure.Timestamp.Ticks.ToString(), new Record<IExpenditure>(Expenditure, new List<string>()));
            return Task.CompletedTask;
        }
    }
}
