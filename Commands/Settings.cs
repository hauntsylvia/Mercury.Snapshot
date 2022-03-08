using izolabella.Discord.Commands.Arguments;
using izolabella.Discord.Commands.Attributes;
using Mercury.Snapshot.Objects.Structures.Personalization;
using Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Commands
{
    internal class Settings
    {
        [Command(new string[] { "settings" })]
        internal static async void ChangeSettings(CommandArguments Args)
        {
            if(Args.Message.Author.Id == 528750326107602965)
            {
                Program.MercuryUser.Settings = new(new MercuryUserSettings(new GoogleCalendarSettings(), new GoogleSheetsSettings("1f6vea1vR4MQ9ts88cebJPhxEc675pyX7St32DJyo7Cg")), new List<string>());
                await Args.Message.Channel.SendMessageAsync("changed");
            }
        }
    }
}
