using System.Text;
using MuxLib.MUtility.Collections.Errors;
namespace MuxLib.MUtility.Collections.Tree
{
    public sealed class SegmentTree<T>
    {
        /// <summary>
        /// This class requires an object of type delegate to be passed in during initialization. 
        /// This is required as a segment creation method to create the segment tree.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public delegate T Merger(T a, T b);

        private readonly T[] _tree;

        private readonly T[] _data;

        private Merger Merge { get; }

        public SegmentTree(T[] arr, Merger merge)
        {
            _data = new T[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                _data[i] = arr[i];
            }
            _tree = new T[arr.Length * 4];
            Merge = merge;
            BuildSegmentTree(0, 0, _data.Length - 1);
        }

        public T this[int index] => _data[index];

        public int Size => _data.Length;

        private static int LeftChildOf(int index) => 2 * index + 1;

        private static int RightChildOf(int index) => 2 * index + 2;

        private void BuildSegmentTree(int treeIndex, int l, int r)
        {
            /*Build A Segment Tree for index l to index r at tree-index*/
            if (l == r)
            {
                _tree[treeIndex] = _data[l];
                return;
            }
            var leftTreeIndex = LeftChildOf(treeIndex);
            var rightTreeIndex = RightChildOf(treeIndex);

            var mid = l + (r - l) / 2;
            BuildSegmentTree(leftTreeIndex, l, mid);
            BuildSegmentTree(rightTreeIndex, mid + 1, r);

            /* sum: _tree[tree-index] = _tree[left_tree_index] + _tree[right_tree_index]; */
            _tree[treeIndex] = Merge(_tree[leftTreeIndex], _tree[rightTreeIndex]);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('[');
            for (var i = 0; i < _tree.Length; i++)
            {
                if (_tree[i] != null)
                    sb.Append(_tree[i]);
                else
                    sb.Append("Null");
                if (i != _tree.Length - 1)
                    sb.Append(", ");
            }
            sb.Append(']');
            return sb.ToString();
        }

        public T Query(int queryL, int queryR)
        {
            if (queryL < 0 || queryL >= _data.Length
                    || queryR < 0 || queryR >= _data.Length ||
                        queryL > queryR) /*Border Check*/
                throw new InvalidArgumentError($"Index ({queryL}, {queryR}) is invalid");
            return Query(0, 0, _data.Length - 1, queryL, queryR);
        }

        private T Query(int treeIndex, int l, int r, int queryL, int queryR)
        {
            //在以tree_index为根节点的线段树中[l..r]的范围里，搜索区间[query_l..query_r]的值。
            if (l == queryL && r == queryR)
                return _tree[treeIndex];

            var mid = l + (r - l) / 2;
            int leftTreeIndex = LeftChildOf(treeIndex),
                rightTreeIndex = RightChildOf(treeIndex);
            if (queryL >= mid + 1)
                return Query(rightTreeIndex, mid + 1, r, queryL, queryR);
            if (queryR <= mid)
                return Query(leftTreeIndex, l, mid, queryL, queryR);
            var leftRes = Query(leftTreeIndex, l, mid, queryL, mid);
            var rightRes = Query(rightTreeIndex, mid + 1, r, mid + 1, queryR);
            return Merge(leftRes, rightRes);

        }

        public static void Tester()
        {
            int?[] nums = { -2, 0, 3, -5, 2, -1 };

            var segmentTree = new SegmentTree<int?>(
                    nums, new SegmentTree<int?>.Merger(
                        (int? a, int? b) => a + b
                    )
                );
            System.Console.WriteLine(segmentTree.Query(0, 2));
            System.Console.WriteLine(segmentTree.ToString());
        }
        /// <summary>
        /// Update data[index] to new_ele, and update the tree
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newEle"></param>        
        public void Update(int index, T newEle)
        {
            if (index < 0 || index >= _data.Length)
                throw new InvalidArgumentError($"Index:{index} is invalid.");
            _data[index] = newEle;
            Update(0, 0, _data.Length - 1, index, newEle);

        }

        private void Update(int treeIndex, int l, int r, int index, T ele)
        {
            if (l == r)
            {
                _tree[treeIndex] = ele;
                return;
            }
            var mid = l + (r - l) / 2;
            var leftTreeIndex = LeftChildOf(treeIndex);
            var rightTreeIndex = RightChildOf(treeIndex);
            if (index >= mid + 1)
                Update(rightTreeIndex, mid + 1, r, index, ele);
            else // index <= mid
                Update(leftTreeIndex, l, mid, index, ele);
            _tree[treeIndex] = Merge(_tree[leftTreeIndex], _tree[rightTreeIndex]);
        }
    }
}
