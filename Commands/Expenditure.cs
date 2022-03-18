using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using izolabella.Discord.Internals.Structures.Commands;

namespace Mercury.Snapshot.Commands
{
    public class Expenditure
    {
        [Command(new string[] { "log-expenditure" }, "Log an expenditure.")]
        public static void LogExpenditure(CommandArguments Args, double Amount, string? Category)
        {
            Console.WriteLine(Amount);
            if(Category != null)
            {
                Console.WriteLine(Category);
            }
        }
    }
}
