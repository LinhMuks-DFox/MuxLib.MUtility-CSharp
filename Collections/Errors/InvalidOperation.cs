using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuxLib.MUtility.Collections.Errors
{
    public sealed class InvalidOperation
    : Exception
    {
        public InvalidOperation() : base() { }
        public InvalidOperation(string msg) : base() { }
    }
}
