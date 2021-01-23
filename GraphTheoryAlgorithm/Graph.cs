using System.IO;
using System.Text;
using MuxLib.MUtility.Com.Error;
using System.Collections.Generic;

namespace MuxLib.MUtility.GraphTheoryAlgorithm
{
    /// <summary>
    ///     Graph(Adj-set) implementation;
    ///     Spatial Complexity: O(Vertices + Edges);
    ///     Time Complexities:
    ///     Build-Graph: O(E*logV)
    ///     Check-Vertices is connected: O(logV)
    ///     Check the degree of a Vertex: O(degree(V)) (the worst case: O(V))
    /// </summary>
    public class Graph
    {
        private readonly string _filename;

        /// <summary>
        ///     Build Graph
        /// </summary>
        /// <param name="filename">
        ///     The original file representing the connection of nodes.
        ///     The first number in the file should be non-negative, it will parsed as the number of Vertices,
        ///     the second number in the file should be non-negative too, it will parsed as the number of Edges,
        ///     Then, each of the next two numbers will represent an edge.
        ///     Eg: File content:
        ///     7 9
        ///     1 3
        ///     2 5
        ///     Means the graph has 7 vertices and 9 edges, vertex 1 and vertex 3 is connected, vertex 2 and vertex 5
        ///     is connected.
        /// </param>
        public Graph(string filename)
        {
            _filename = filename;
            using var fi = new StreamReader(filename);
            var content = fi.ReadToEnd();
            var digits = content.Split(',', ' ', '\n', '\r', '\0');
            var index = 0;
            Vertices = int.Parse(digits[index]);
            if (Vertices < 0)
                throw new InvalidArgumentError($"The number of Vertices should be non-negative, but: {Vertices}");
            index++;
            Edges = int.Parse(digits[index]);
            if (Edges < 0)
                throw new InvalidArgumentError($"The number of Edges should be non-negative, but: {Edges}");
            index++;

            Adj = new List<SortedSet<int>>(Vertices);

            for (var i = 0; i < Vertices; ++i) Adj.Add(new SortedSet<int>());

            while (index < digits.Length - 1)
            {
                var a = int.Parse(digits[index]);
                IsValidVertex(a);
                var b = int.Parse(digits[index + 1]);
                IsValidVertex(b);
                if (a == b) // 不处理自环边
                    throw new InvalidArgumentError("Self loop is detected!");
                if (Adj[a].Contains(b)) // 不处理平行边， 当然可以用delegate select(int x, int y)来从平行边中进行筛选
                    throw new InvalidArgumentError("Parallel edge was detected!");
                Adj[a].Add(b);
                Adj[b].Add(a);
                index += 2;
            }
        }

        public Graph(Graph set) : this(set._filename)
        {
        }

        /// <summary>
        ///     the number of vertices
        /// </summary>
        public int Vertices { get; }

        /// <summary>
        ///     the number of edges.
        /// </summary>
        public int Edges { get; }

        /// <summary>
        ///     The adj-set.
        /// </summary>
        private List<SortedSet<int>> Adj { get; }

        private void IsValidVertex(int v)
        {
            if (v < 0 || v >= Vertices)
                throw new InvalidArgumentError($"Vertex {v} is invalid");
        }

        public bool HasEdge(int v, int w)
        {
            IsValidVertex(v);
            IsValidVertex(w);
            return Adj[v].Contains(w);
        }

        public IEnumerable<int> AdjacentOf(int v)
        {
            IsValidVertex(v);
            return Adj[v];
        }

        /// <summary>
        ///     Get the degree of v
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int Degree(int v)
        {
            IsValidVertex(v);
            return Adj[v].Count;
        }

        /// <summary>
        ///     Visualize AdjList in string-form;
        /// </summary>
        /// <returns>AdjList in string-form</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Vertices = {Vertices}, Edges = {Edges}\n");
            for (var i = 0; i < Vertices; ++i)
            {
                sb.Append($"{i}: ");
                foreach (var v in Adj[i]) sb.Append($"{v} ");
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}