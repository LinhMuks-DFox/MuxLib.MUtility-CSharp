using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuxLib.MUtility.Collections.List.LinkedList
{
    internal class Node<T>
    {
        public T Data { set; get; } = default;

        public Node<T> Next { set; get; } = null;

        public Node<T> Before { set; get; } = null;

        public Node() { }

        public Node(Node<T> next, Node<T> prev, T data) => (Next, Before, Data) 
                                                        = (next, prev, data);

        public Node(T data) { Data = data; }

        public Node(Node<T> next, Node<T> prev) => (Next, Before) = (next, prev);

    }
}
