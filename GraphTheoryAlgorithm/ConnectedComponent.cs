namespace MuxLib.MUtility.GraphTheoryAlgorithm
{
    public class ConnectedComponent
    {
        private readonly bool[] _visited;
        private readonly Graph _g;

        public ConnectedComponent(Graph g)
        {
            _g = g;
            _visited = new bool[g.Vertices];
            for (var i = 0; i < _visited.Length; ++i)
                _visited[i] = false;
            for (var i = 0; i < g.Vertices; ++i)
                if (!_visited[i])
                {
                    Dfs(i);
                    ConnectedComponents++;
                }
        }

        public int ConnectedComponents { get; } = 0;

        private void Dfs(int v)
        {
            _visited[v] = true;
            foreach (var w in _g.AdjacentOf(v))
                if (!_visited[w])
                    Dfs(w);
        }

    }
}