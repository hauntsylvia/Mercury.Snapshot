using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.UserStructures.Interfaces
{
    public interface ISyncable
    {
        public Task Pull();
        public Task Push();
    }
}
