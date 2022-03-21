using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Consts
{
    internal static class EmbedDefaults
    {
        internal static EmbedBuilder FullEmbed => new()
        {
            Color = new Color(0xe5e5e5),
            Timestamp = DateTime.UtcNow,
            Footer = new()
            {
                Text = "-☿-"
            }
        };
    }
}
