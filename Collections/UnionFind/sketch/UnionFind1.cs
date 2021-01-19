using MuxLib.MUtility.Collections.Errors;
using MuxLib.MUtility.Collections.Metas.ABClass;

namespace MuxLib.MUtility.Collections.UnionFind
{
    public class UnionFind1 :
        ABCUnionFind
    {
        public int[] _Id;

        public UnionFind1(int size)
        {
            _Id = new int[size];
            for (var i = 0; i < _Id.Length; i++)
                _Id[i] = i;
        }

        public override int Size => _Id.Length;

        private int Find(int p)
        {
            if (p < 0 && p >= _Id.Length)
                throw new InvalidArgumentError($"p({p}) is out of range ");
            return _Id[p];
        }

        public override bool IsConnected(int item_p, int item_q)
        {
            return Find(item_p) == Find(item_q);
        }

        public override void UnionElements(int item_p, int item_q)
        {
            int pID = Find(item_p), qID = Find(item_q);
            if (pID == qID)
                return;
            for (var i = 0; i < _Id.Length; i++)
                if (_Id[i] == pID)
                    _Id[i] = qID;
        }
    }
}