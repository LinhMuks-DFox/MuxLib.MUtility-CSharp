using System;

namespace MuxLib.MUtility.Com.Error
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
    }
}