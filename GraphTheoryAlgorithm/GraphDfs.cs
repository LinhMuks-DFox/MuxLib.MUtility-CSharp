using System.Collections.Generic;

namespace MuxLib.MUtility.GraphTheoryAlgorithm
{
    /// <summary>
    /// O(V + E)
    /// </summary>
    public class GraphDfs
    {
        private readonly List<int> _order;
        private readonly bool[] _visited;
        private readonly Graph _g;

        public GraphDfs(Graph g)
        {
            _order = new List<int>(g.Vertices);
            _g = g;
            _visited = new bool[g.Vertices];
            for (var i = 0; i < _visited.Length; ++i)
                _visited[i] = false;
            for (var i = 0; i < g.Vertices; ++i)
                if (!_visited[i])
                    Dfs(i);
        }

        private void Dfs(int v)
        {
            _visited[v] = true;
            _order.Add(v);
            foreach (var w in _g.AdjacentOf(v))
                if (!_visited[w])
                    Dfs(w);
        }

        public IEnumerable<int> Order => _order;
    }
}