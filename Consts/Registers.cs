using Mercury.Unification.IO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Consts
{
    internal class Registers
    {
        internal readonly static Register GoogleCredentialsRegister = new("Google User Credentials");
        internal readonly static Register MercurySettingsRegister = new("Mercury User Settings");
    }
}
