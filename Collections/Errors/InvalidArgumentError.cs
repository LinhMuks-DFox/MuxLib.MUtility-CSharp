﻿using System;

namespace MuxLib.MUtility.Collections.Errors
{
    public sealed class InvalidArgumentError
        : Exception
    {
        public InvalidArgumentError() : base() { }

        public InvalidArgumentError(string msg) : base(msg) { }
    }
}
