using System;

namespace MuxLib.MUtility.Com.Error
{
    public sealed class InvalidArgumentError
        : Exception
    {
        public InvalidArgumentError() : base() { }

        public InvalidArgumentError(string msg) : base(msg) { }
    }
}
