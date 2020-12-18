using MuxLib.MUtility.Collections.Table;
namespace MuxLib.MUtility.Collections.Set
{
    public sealed class HashSet<T> : Metas.ABClass.ABCSet<T>
    {
        private readonly HashTable<T, object> _datas;
        public override int Size => _datas.Size;

        public override bool Empty => Size == 0;

        public HashSet()
        {
            _datas = new HashTable<T, object>();
        }

        public override void Add(T e)
        {
            _datas.Append(e, null);
        }

        public override bool Contains(T e)
        {
            return _datas.Contains(e);
        }

        public override void Remove(T e)
        {
            _datas.Remove(e);
        }
    }
}
