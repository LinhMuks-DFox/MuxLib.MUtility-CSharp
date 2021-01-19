/*
 * Time:        <2020/8/21>
 * Author:      dotFoxyyyy
 * Summary:     ABCStack
 * CopyRight:   dotFoxyyyy-2020
 */

using System.Collections.Generic;

namespace MuxLib.MUtility.Collections.Metas.ABClass
{
    public abstract class ABCStack<T>
    {
        public abstract int Size { get; }

        public abstract bool Empty { get; }
        public abstract void Push(T e);
        public abstract T Pop();

        public abstract T Peek();

        public abstract void Load(IEnumerable<T> meta_array);
    }
}