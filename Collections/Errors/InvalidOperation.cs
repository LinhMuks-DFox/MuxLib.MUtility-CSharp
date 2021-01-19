using System;

namespace MuxLib.MUtility.Collections.Errors
{
    public sealed class InvalidOperation
        : Exception
    {
        public InvalidOperation()
        {
        }

        public InvalidOperation(string msg)
        {
        }

        public InvalidOperation(Exception ex) : base(ex.Message)
        {
        }
    }
}