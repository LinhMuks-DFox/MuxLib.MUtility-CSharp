using System;

namespace MuxLib.MUtility.Collections.Errors
{
    public sealed class InvalidArgumentError
        : Exception
    {
        public InvalidArgumentError()
        {
        }

        public InvalidArgumentError(string msg) : base(msg)
        {
        }

        public InvalidArgumentError(Exception ex) : base(ex.Message)
        {
        }
    }
}