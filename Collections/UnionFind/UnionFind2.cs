namespace MuxLib.MUtility.Collections.UnionFind
{
    public class UnionFind2 : Metas.ABClass.ABCUnionFind
    {
        private int[] _parent;

        public UnionFind2(int size)
        {
            _parent = new int[size];
            for (int i = 0; i < size; i++)
                _parent[i] = i;
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
            _parent[pRoot] = qRoot;
        }
    }
}
