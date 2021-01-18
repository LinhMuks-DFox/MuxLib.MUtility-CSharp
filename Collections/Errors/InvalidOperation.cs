using System;

namespace MuxLib.MUtility.Collections.Errors
{
    public sealed class InvalidOperation
    : Exception
    {
        public InvalidOperation() : base() { }
        public InvalidOperation(string msg) : base() { }
        
        public InvalidOperation(Exception ex) : base(ex.Message){}
    }
}
