namespace MuxLib.MUtility.Collections.UnionFind
{
    public class UnionFind3 : Metas.ABClass.ABCUnionFind
    {
        private readonly int[] _parent;
        private readonly int[] _size;

        public UnionFind3(int size)
        {
            _parent = new int[size];
            _size = new int[size];
            for (int i = 0; i < size; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
            
        }

        public override int Size { get => _parent.Length; }

        // O(h)
        private int Find(int p)
        {
            if (p < 0 && p >= _parent.Length)
                throw new Errors.InvalidArgumentError($"Argument p:{p} is invalid.");
            while (p != _parent[p])
                p = _parent[p];

            return p;
        }

        public override bool IsConnected(int item_p, int item_q) =>
            Find(item_p) == Find(item_q);


        public override void UnionElements(int item_p, int item_q)
        {
            int pRoot = Find(item_p), qRoot = Find(item_q);
            if (pRoot == qRoot)
                return;

            if (_size[pRoot] < _size[qRoot])
            {
                _parent[pRoot] = qRoot;
                _size[qRoot] += _size[pRoot];
            }
            else 
            {
                _parent[qRoot] = pRoot;
                _size[pRoot] += _size[qRoot];
            }

        }
    }
}

