namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCOrderSymbolTable<K, V> : ABCSymbolTable<K, V>
        where K : System.IComparable
    {
        public abstract K Min();

        public abstract K Max();

        /// <summary>
        /// Less than key but the biggest
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract K Floor(K key);

        /// <summary>
        /// Greater than key but Less-est;
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract K Ceiling(K key);

        /// <summary>
        /// The number of keys which less than key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract int Rank(K key);

        /// <summary>
        /// Get the key with rank k
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract K Select(int k);

        public abstract void DeleteMin();

        public abstract void DeleteMax();

        /// <summary>
        /// Check the number of Keys between low and max
        /// </summary>
        /// <param name="low"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public abstract int NumberOf(K low, K max);

        /// <summary>
        /// Get a IEnumerable<K> object of keys between low and hight 
        /// </summary>
        /// <param name="low"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public abstract System.Collections.Generic.IEnumerable<K> SortedKeys(K low, K max);

    }
}
