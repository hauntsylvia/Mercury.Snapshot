using Mercury.Snapshot.Consts.Enums;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;
using Mercury.Unification.Util.ObjectComparisons;
using System.Reflection;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    public class MercuryExpenditureLog : IExpenditureLog, ISyncable
    {
        public MercuryExpenditureLog(MercuryUser User)
        {
            this.User = User;
        }

        public MercuryUser User { get; }

        public Task<IReadOnlyCollection<ExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults)
        {
            List<ExpenditureEntry> Events = new();
            if (this.User.ExpenditureEntriesRegister != null)
            {
                Events.AddRange(this.User.ExpenditureEntriesRegister.GetAllRecords().Select(X => X.ObjectToStore).Where(Z => Z.Timestamp >= TimeMin && Z.Timestamp <= TimeMax));
            }
            return Task.FromResult<IReadOnlyCollection<ExpenditureEntry>>(Events.SkipLast(Events.Count - MaxResults).ToList());
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

        public async Task Pull()
        {
            IReadOnlyCollection<ExpenditureEntry> MercuryExps = await this.GetExpenditures(DateTime.MinValue, DateTime.Today.AddYears(1), int.MaxValue);
            foreach (IExpenditureLog? ExpLog in this.User.ExpenditureLogs)
            {
                if (ExpLog != null && ExpLog.GetType() != typeof(MercuryExpenditureLog))
                {
                    IReadOnlyCollection<ExpenditureEntry> ThisLogExpsEntries = await ExpLog.GetExpenditures(DateTime.MinValue, DateTime.Today.AddYears(1), int.MaxValue);
                    foreach (ExpenditureEntry CEntry in ThisLogExpsEntries)
                    {
                        ExpenditureEntry? MatchingEvent = MercuryExps.FirstOrDefault(MEntry => ObjectEqualityManager.PropertiesAreEqual(MEntry, CEntry));
                        if (MatchingEvent == null)
                        {
                            await this.SaveExpenditures(CEntry);
                        }
                    }
                }
            }
        }

        public async Task Push()
        {
            IReadOnlyCollection<ExpenditureEntry> MercuryExps = await this.GetExpenditures(DateTime.MinValue, DateTime.MaxValue, int.MaxValue).ConfigureAwait(false);
            foreach (IExpenditureLog? ExpLog in this.User.ExpenditureLogs)
            {
                if (ExpLog != null)
                {
                    IReadOnlyCollection<ExpenditureEntry> EntriesOfThisLog = await ExpLog.GetExpenditures(DateTime.MinValue, DateTime.MaxValue, int.MaxValue);
                    foreach (ExpenditureEntry Exp in MercuryExps)
                    {
                        if (Exp.Origin == Origins.Mercury && EntriesOfThisLog.All(E => E.Id != Exp.Id))
                        {
                            await ExpLog.SaveExpenditures(Exp).ConfigureAwait(false);
                        }
                    }
                }
            }
        }
    }
}
