using System.IO;
using System.Text;

namespace MuxLib.MUtility.GraphTheoryAlgorithm
{
    /// <summary>
    /// Adjacency matrix
    /// </summary>
    public class AdjMatrix
    {
        /// <summary>
        /// Build AdjMatrix
        /// </summary>
        /// <param name="filename">The original file representing the connection of nodes. See the file format in Doc</param>
        public AdjMatrix(string filename)
        {
            using var fi = new StreamReader(filename);
            var content = fi.ReadToEnd();
            var digits = content.Split(',', ' ', '\n', '\r', '\0');
            var index = 0;
            Vertices = int.Parse(digits[index]);
            index++;
            Edges = int.Parse(digits[index]);
            index++;
            Adj = new int[Vertices, Vertices];
            while (index < digits.Length - 1)
            {
                int a = int.Parse(digits[index]),
                    b = int.Parse(digits[index + 1]);
                Adj[a, b] = 1;
                Adj[b, a] = 1;
                index += 2;
            }
        }

        /// <summary>
        /// the number of vertices 
        /// </summary>
        private int Vertices { get; }

        /// <summary>
        /// the number of edges.
        /// </summary>
        private int Edges { get; }

        /// <summary>
        /// The adj-matrix.
        /// </summary>
        private int[,] Adj { get; }

        /// <summary>
        /// Visualize AdjMatrix in string-form;
        /// </summary>
        /// <returns>AdjMatrix in string-form</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Vertices = {Vertices}, Edges = {Edges}\n");
            for (var i = 0; i < Vertices; i++)
            {
                for (var j = 0; j < Vertices; j++) sb.Append($"{Adj[i, j]} ");
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}