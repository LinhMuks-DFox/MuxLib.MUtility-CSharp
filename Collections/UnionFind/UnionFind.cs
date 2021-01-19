using MuxLib.MUtility.Collections.Errors;
using MuxLib.MUtility.Collections.Metas.ABClass;

namespace MuxLib.MUtility.Collections.UnionFind
{
    public sealed class UnionFind : ABCUnionFind
    {
        private readonly int[] _parent;

        // private readonly int[] _size;
        private readonly int[] _rank;

        public UnionFind(int size)
        {
            _parent = new int[size];
            // _size = new int[size];
            _rank = new int[size];
            for (var i = 0; i < size; i++)
            {
                _parent[i] = i;
                // _size[i] = 1;
                _rank[i] = 1;
            }
        }

        public override int Size => _parent.Length;

        // O(h)
        private int Find(int p)
        {
            if (p < 0 && p >= _parent.Length)
                throw new InvalidArgumentError($"Argument p:{p} is invalid.");
            if (p != _parent[p])
                _parent[p] = Find(_parent[p]);
            return _parent[p];
        }

        public override bool IsConnected(int itemP, int itemQ)
        {
            return Find(itemP) == Find(itemQ);
        }


        public override void UnionElements(int itemP, int itemQ)
        {
            int pRoot = Find(itemP), qRoot = Find(itemQ);
            if (pRoot == qRoot)
                return;
            if (_rank[pRoot] < _rank[qRoot])
            {
                _parent[pRoot] = qRoot;
                // _size[qRoot] += _size[pRoot];
            }
            else if (_rank[qRoot] < _rank[pRoot])
            {
                _parent[qRoot] = pRoot;
            }
            else // ==
            {
                _parent[qRoot] = pRoot;
                // _size[pRoot] += _size[qRoot];
                _rank[pRoot]++;
            }
        }
    }
}