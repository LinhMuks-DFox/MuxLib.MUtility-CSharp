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
        abstract public int Size { get; }

        abstract public bool Empty { get; }
        abstract public void Push(T e);
        abstract public T Pop();

        abstract public T Peek();

        abstract public void Load(IEnumerable<T> meta_array);

    }
}
