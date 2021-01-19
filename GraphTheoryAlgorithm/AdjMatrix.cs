using System;
using System.IO;
using System.Text;

namespace MuxLib.MUtility.GraphTheoryAlgorithm
{
    public class AdjMatrix
    {
        private int V { set; get; }
        private int E { set; get; }
        private int[,] Adj { set; get; }

        public AdjMatrix(string filename)
        {
            using var fi = new StreamReader(filename);
            string content = fi.ReadToEnd();
            string[] digits = content.Split(new char[] {',', ' ', '\n', '\r', '\0'});
            V = int.Parse(digits[0]);
            E = int.Parse(digits[1]);
            Adj = new int[V, V];
            for (var i = 0; i < digits.Length; ++i)
            {
                int a = int.Parse(digits[i]);
                int b = int.Parse(digits[i + 1]);

                Adj[a, b] = 1;
                Adj[b, a] = 1;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"V = {V}, E = {E} ]\n");
            for (var i = 0; i < V; i++)
            {
                for (var j = 0; j < V; j++) sb.Append($"{Adj[i, j]} ");
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}