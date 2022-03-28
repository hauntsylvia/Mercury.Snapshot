using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Consts
{
    public static class EmbedDefaults
    {
        public static EmbedBuilder FullEmbed => new()
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
