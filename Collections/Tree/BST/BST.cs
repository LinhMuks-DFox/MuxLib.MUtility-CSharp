using System;
using System.Collections.Generic;
using System.Text;
namespace MuxLib.MUtility.Collections.Tree.BST
{
    public sealed class BST<E> where E : IComparable
    {

        private class Node
        {
            public E e;
            public Node Left { set; get; } = null;
            public Node Right { set; get; } = null;

            public Node(E e)
            {
                this.e = e;
            }
        }

        private Node _root;
        private int _size;

        public BST()
        {
            _root = null;
            _size = 0;
        }

        public int Size { get => _size; }

        public bool isEmpty { get => _size == 0; }

        // 向二分搜索树中添加新的元素e
        public void Add(E e)
        {
            _root = Add(_root, e);
        }

        // 向以node为根的二分搜索树中插入元素e，递归算法
        // 返回插入新节点后二分搜索树的根
        private Node Add(Node node, E e)
        {

            if (node == null)
            {
                _size++;
                return new Node(e);
            }

            if (e.CompareTo(node.e) < 0)
                node.Left = Add(node.Left, e);
            else if (e.CompareTo(node.e) > 0)
                node.Right = Add(node.Right, e);

            return node;
        }

        // 看二分搜索树中是否包含元素e
        public bool Contains(E e)
        {
            return Contains(_root, e);
        }

        // 看以node为根的二分搜索树中是否包含元素e, 递归算法
        private bool Contains(Node node, E e)
        {

            if (node == null)
                return false;

            if (e.CompareTo(node.e) == 0)
                return true;
            else if (e.CompareTo(node.e) < 0)
                return Contains(node.Left, e);
            else // e.CompareTo(node.e) > 0
                return Contains(node.Right, e);
        }

        // 二分搜索树的前序遍历
        public void PreOrder()
        {
            PreOrder(_root);
        }

        // 前序遍历以node为根的二分搜索树, 递归算法
        private void PreOrder(Node node)
        {

            if (node == null)
                return;

            Console.WriteLine(node.e);
            PreOrder(node.Left);
            PreOrder(node.Right);
        }

        // 二分搜索树的非递归前序遍历
        public void PreOrderNR()
        {

            Stack<Node> stack = new Stack<Node>();
            stack.Push(_root);
            while (stack.Count != 0)
            {
                Node cur = stack.Pop();
                Console.WriteLine(cur.e);

                if (cur.Right != null)
                    stack.Push(cur.Right);
                if (cur.Left != null)
                    stack.Push(cur.Left);
            }
        }

        // 二分搜索树的中序遍历
        public void InOrder()
        {
            InOrder(_root);
        }

        // 中序遍历以node为根的二分搜索树, 递归算法
        private void InOrder(Node node)
        {

            if (node == null)
                return;

            InOrder(node.Left);
            Console.WriteLine(node.e);
            InOrder(node.Right);
        }

        // 二分搜索树的后序遍历
        public void PostOrder()
        {
            PostOrder(_root);
        }

        // 后序遍历以node为根的二分搜索树, 递归算法
        private void PostOrder(Node node)
        {

            if (node == null)
                return;

            PostOrder(node.Left);
            PostOrder(node.Right);
            Console.WriteLine(node.e);
        }

        // 二分搜索树的层序遍历
        public void LevelOrder()
        {

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(_root);
            while (q.Count != 0)
            {
                Node cur = q.Dequeue();
                Console.WriteLine(cur.e);

                if (cur.Left != null)
                    q.Enqueue(cur.Left);
                if (cur.Right != null)
                    q.Enqueue(cur.Right);
            }
        }

        // 寻找二分搜索树的最小元素
        public E Minimum()
        {
            if (_size == 0)
                throw new Errors.InvalidArgumentError("BST is empty!");

            return Minimum(_root).e;
        }

        // 返回以node为根的二分搜索树的最小值所在的节点
        private Node Minimum(Node node)
        {
            if (node.Left == null)
                return node;
            return Minimum(node.Left);
        }

        // 寻找二分搜索树的最大元素
        public E Maximum()
        {
            if (_size == 0)
                throw new Errors.InvalidArgumentError("BST is empty");

            return Maximum(_root).e;
        }

        // 返回以node为根的二分搜索树的最大值所在的节点
        private Node Maximum(Node node)
        {
            if (node.Right == null)
                return node;

            return Maximum(node.Right);
        }

        // 从二分搜索树中删除最小值所在节点, 返回最小值
        public E RemoveMin()
        {
            E ret = Minimum();
            _root = RemoveMin(_root);
            return ret;
        }

        // 删除掉以node为根的二分搜索树中的最小节点
        // 返回删除节点后新的二分搜索树的根
        private Node RemoveMin(Node node)
        {

            if (node.Left == null)
            {
                Node rightNode = node.Right;
                node.Right = null;
                _size--;
                return rightNode;
            }

            node.Left = RemoveMin(node.Left);
            return node;
        }

        // 从二分搜索树中删除最大值所在节点
        public E RemoveMax()
        {
            E ret = Maximum();
            _root = RemoveMax(_root);
            return ret;
        }

        // 删除掉以node为根的二分搜索树中的最大节点
        // 返回删除节点后新的二分搜索树的根
        private Node RemoveMax(Node node)
        {

            if (node.Right == null)
            {
                Node leftNode = node.Left;
                node.Left = null;
                _size--;
                return leftNode;
            }

            node.Right = RemoveMax(node.Right);
            return node;
        }

        // 从二分搜索树中删除元素为e的节点
        public void Remove(E e)
        {
            _root = Remove(_root, e);
        }

        // 删除掉以node为根的二分搜索树中值为e的节点, 递归算法
        // 返回删除节点后新的二分搜索树的根
        private Node Remove(Node node, E e)
        {

            if (node == null)
                return null;

            if (e.CompareTo(node.e) < 0)
            {
                node.Left = Remove(node.Left, e);
                return node;
            }
            else if (e.CompareTo(node.e) > 0)
            {
                node.Right = Remove(node.Right, e);
                return node;
            }
            else
            {   // e.CompareTo(node.e) == 0

                // 待删除节点左子树为空的情况
                if (node.Left == null)
                {
                    Node rightNode = node.Right;
                    node.Right = null;
                    _size--;
                    return rightNode;
                }

                // 待删除节点右子树为空的情况
                if (node.Right == null)
                {
                    Node leftNode = node.Left;
                    node.Left = null;
                    _size--;
                    return leftNode;
                }

                // 待删除节点左右子树均不为空的情况

                // 找到比待删除节点大的最小节点, 即待删除节点右子树的最小节点
                // 用这个节点顶替待删除节点的位置
                Node successor = Minimum(node.Right);
                successor.Right = RemoveMin(node.Right);
                successor.Left = node.Left;

                node.Left = node.Right = null;

                return successor;
            }
        }


        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            GenerateBSTString(_root, 0, res);
            return res.ToString();
        }

        // 生成以node为根节点，深度为depth的描述二叉树的字符串
        private void GenerateBSTString(Node node, int depth, StringBuilder res)
        {

            if (node == null)
            {
                res.Append(GenerateDepthString(depth) + "null\n");
                return;
            }

            res.Append(GenerateDepthString(depth) + node.e + "\n");
            GenerateBSTString(node.Left, depth + 1, res);
            GenerateBSTString(node.Right, depth + 1, res);
        }

        private string GenerateDepthString(int depth)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < depth; i++)
                res.Append("--");
            return res.ToString();
        }

    }
}
