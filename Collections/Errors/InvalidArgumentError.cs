using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuxLib.MUtility.Collections.Errors
{
    public sealed class InvalidArgumentError
        : Exception
    {
        public InvalidArgumentError() : base() { }

        public InvalidArgumentError(string msg) : base(msg) { }
    }
}
