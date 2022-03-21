using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using izolabella.Discord.Internals.Structures.Commands;
using Mercury.Snapshot.Objects.Structures.UserStructures.Financial.Entries;
using Mercury.Snapshot.Objects.Structures.UserStructures.Identification;
using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;
using Mercury.Unification.IO.File.Records;

namespace Mercury.Snapshot.Commands
{
    public class Expenditure
    {
        [Command(new string[] { "log-expenditure" }, "Log an expenditure.")]
        public static void LogExpenditure(CommandArguments Args, SocketGuildUser? User1, double Amount, string PaidTo, string Category)
        {
            MercuryUser User = new(Args.SlashCommand.User.Id);
            if(User.ExpenditureEntriesRegister != null)
            {
                MercuryExpenditureEntry Entry = new(DateTime.UtcNow, Amount, PaidTo, Category ?? string.Empty, Origins.Mercury, Identifier.GetIdentifier());
                User.ExpenditureEntriesRegister.SaveRecord(Entry.Id, new Record<IExpenditureEntry>(Entry, null));
            }
        }
    }
}
