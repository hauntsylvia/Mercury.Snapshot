using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Exceptions
{
    public class MercuryColdStartupException : Exception
    {
        public MercuryColdStartupException(string? message) : base(message)
        {
        }
    }
}
