using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces
{
    internal interface ISyncable
    {
        internal Task Pull();
        internal Task Push();
    }
}
