namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCUnionFind
    {
        public abstract int Size { get; }
        public abstract bool IsConnected(int item_p, int item_q);

        public abstract void UnionElements(int item_p, int item_q);
    }
}