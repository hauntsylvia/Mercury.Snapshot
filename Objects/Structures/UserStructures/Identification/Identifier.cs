using Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces;
using Mercury.Unification.IO.File.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Identification
{
    internal static class Identifier
    {
        private static ulong lastContender = (ulong)(DateTime.UtcNow - DateTime.UnixEpoch).TotalMilliseconds;
        internal static ulong GetIdentifier()
        {
            lastContender = Interlocked.Increment(ref lastContender);
            return lastContender;
        }
    }
}
