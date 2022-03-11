using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Commands
{
    public class Example
    {
        [Command(new string[] { "a" }, "Example command.")]
        public static void Abc(CommandArguments Args)
        {
            Console.Out.WriteLine(Args.SlashCommand.User.Username);
        }
    }
}
