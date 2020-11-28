using System;


namespace MuxLib.MUtility.Collections.Errors
{
    public class ResizingError:Exception
    {
        public ResizingError() : base() { }
        public ResizingError(string msg) : base(msg) { }
    }
}
