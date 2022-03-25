using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;
using Mercury.Unification.Util.ObjectComparisons;
using System.Reflection;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Financial
{
    internal class MercuryExpenditureLog : IExpenditureLog, IMercuryExpenditureLog, ISyncable
    {
        internal MercuryExpenditureLog(MercuryUser User)
        {
            this.User = User;
        }

        internal MercuryUser User { get; }

        internal Task<IReadOnlyCollection<ExpenditureEntry>> GetExpenditures(DateTime TimeMin, DateTime TimeMax, int MaxResults)
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

        internal Task SaveExpenditures(params ExpenditureEntry[] Entries)
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

        private static Task<List<ExpenditureEntry>> GetAllNonMatchingAsync(ExpenditureEntry[] A, ExpenditureEntry[] B)
        {
            List<ExpenditureEntry> Expenditures = new();
            foreach (ExpenditureEntry ExpA in A)
            {
                ExpenditureEntry? MatchingExpenditure = B.FirstOrDefault(E => ObjectEqualityManager.PropertiesAreEqual(E, ExpA));
                if (MatchingExpenditure == null)
                {
                    Expenditures.Add(ExpA);
                }
            }
            return Task.FromResult(Expenditures);
        }

        private async Task RecursivePull(DateTime Min, DateTime Max, List<IExpenditureLog?> LogsToSync, ExpenditureEntry[] Buffer)
        {
            IReadOnlyCollection<ExpenditureEntry> MercuryExpEntries = await this.GetExpenditures(Min, Max, Buffer.Length).ConfigureAwait(false);
            foreach (IExpenditureLog? ExpLog in LogsToSync.ToList())
            {
                if (ExpLog != null)
                {
                    Buffer = (await ExpLog.GetExpenditures(Min, Max, Buffer.Length).ConfigureAwait(false)).ToArray();
                    List<ExpenditureEntry> NonMatching = await GetAllNonMatchingAsync(Buffer, MercuryExpEntries.ToArray()).ConfigureAwait(false);
                    if (Buffer.All(X => X != null) && NonMatching.Count > 0)
                    {
                        await this.SaveExpenditures(NonMatching.ToArray()).ConfigureAwait(false);
                    }
                    else
                    {
                        LogsToSync.Remove(ExpLog);
                    }
                    await this.RecursivePull(NonMatching.Count > 0 ? NonMatching.Last().Timestamp : Min, Max, LogsToSync, new ExpenditureEntry[Buffer.Length]).ConfigureAwait(false);
                }
            }
        }

        internal async Task Pull()
        {
            await this.RecursivePull(DateTime.MinValue, DateTime.MaxValue, this.User.ExpenditureLogs.ToList(), new ExpenditureEntry[2048]).ConfigureAwait(false);
        }

        internal async Task Push()
        {
            IReadOnlyCollection<ExpenditureEntry> MercuryExps = await this.GetExpenditures(DateTime.MinValue, DateTime.MaxValue, int.MaxValue).ConfigureAwait(false);
            foreach (IExpenditureLog? ExpLog in this.User.Calendars)
            {
                if (ExpLog != null)
                {
                    foreach (ExpenditureEntry Exp in MercuryExps)
                    {
                        if (Exp.Origin == Origins.Mercury)
                        {
                            await ExpLog.SaveExpenditures(Exp).ConfigureAwait(false);
                        }
                    }
                }
            }
        }
    }
}
