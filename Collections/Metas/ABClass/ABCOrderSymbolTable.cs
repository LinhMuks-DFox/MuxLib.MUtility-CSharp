using System;
using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCOrderSymbolTable<TK, TV> : ABCSymbolTable<TK, TV>
        where TK : IComparable
    {
        public abstract TK Min();

        public abstract TK Max();

        /// <summary>
        ///     Less than key but the biggest
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract TK Floor(TK key);

        /// <summary>
        ///     Greater than key but Less-est;
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract TK Ceiling(TK key);

        /// <summary>
        ///     The number of keys which less than key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract int Rank(TK key);

        /// <summary>
        ///     Get the key with rank k
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public abstract TK Select(int k);

        public abstract void DeleteMin();

        public abstract void DeleteMax();

        /// <summary>
        ///     Check the number of Keys between low and max
        /// </summary>
        /// <param name="low"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public abstract int NumberOf(TK low, TK max);

        /// <summary>
        ///     Get a IEnumerable<K> object of keys between low and hight
        /// </summary>
        /// <param name="low"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public abstract IEnumerable<TK> SortedKeys(TK low, TK max);
    }
}