using System.Collections.Generic;
using System.IO;

namespace MuxLib.MUtility.Com.FileHelpers
{
    /// <summary>
    ///     Read a pure English text and split it to words;
    /// </summary>
    public sealed class TextReader
    {
        public TextReader()
        {
            Word = new List<string>();
        }

        public List<string> Word { get; }

        public void Read(string path)
        {
            if (!File.Exists(path)) throw new IOException($"{path} was not exist;");
            using var fi = new StreamReader(path);
            var buffer = "";
            while (fi.Peek() >= 0)
            {
                var cur = (char) fi.Read();
                if (!IsLetters(cur) || cur == '0' || cur == '\n' || cur == '\r')
                {
                    Word.Add(buffer);
                    buffer = "";
                    continue;
                }

                buffer += cur;
            }
        }

        private static bool IsLetters(char ch)
        {
            return 'A' <= ch && ch <= 'Z' || 'a' <= ch && ch <= 'z' || ch == '-' || ch == '_' || ch == '\'';
        }
    }
}