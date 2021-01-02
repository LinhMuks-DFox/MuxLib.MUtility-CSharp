using System;

namespace MuxLib.MUtility.Com.Error
{
    public sealed class InvalidOperation
    : Exception
    {
        public InvalidOperation() : base() { }
        public InvalidOperation(string msg) : base() { }
    }
}
