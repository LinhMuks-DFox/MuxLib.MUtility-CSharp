using System.IO;
using System.Text;

namespace MuxLib.MUtility.GraphTheoryAlgorithm
{
    public class AdjMatrix
    {
        public AdjMatrix(string filename)
        {
            using var fi = new StreamReader(filename);
            var content = fi.ReadToEnd();
            var digits = content.Split(',', ' ', '\n', '\r', '\0');
            var index = 0;
            V = int.Parse(digits[index]);
            index++;
            E = int.Parse(digits[index]);
            index++;
            Adj = new int[V, V];
            for (; index < digits.Length - 1; index += 2)
            {
                var a = int.Parse(digits[index]);
                var b = int.Parse(digits[index + 1]);
                Adj[a, b] = 1;
                Adj[b, a] = 1;
            }
        }

        private int V { get; }
        private int E { get; }
        private int[,] Adj { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"V = {V}, E = {E}\n");
            for (var i = 0; i < V; i++)
            {
                for (var j = 0; j < V; j++) sb.Append($"{Adj[i, j]} ");
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}