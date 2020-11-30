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

        public T this[int index]
        {
            get => _data[index];
        }

        public int Size { get => _data.Length; }

        private static int LeftChildOf(int index) => 2 * index + 1;

        private static int RightChildOf(int index) => 2 * index + 2;

        private void BuildSegmentTree(int treeindex, int l, int r)
        {
            /*Build A Segment Tree for index l to index r at tree-index*/
            if (l == r)
            {
                _tree[treeindex] = _data[l];
                return;
            }
            int left_tree_index = LeftChildOf(treeindex);
            int right_tree_index = RightChildOf(treeindex);

            int mid = l + (r - l) / 2;
            BuildSegmentTree(left_tree_index, l, mid);
            BuildSegmentTree(right_tree_index, mid + 1, r);

            /*sum: _tree[tree-index] = _tree[left_tree_index] + _tree[right_tree_index];*/
            _tree[treeindex] = Merge(_tree[left_tree_index], _tree[right_tree_index]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _tree.Length; i++)
            {
                if (_tree[i] != null)
                    sb.Append(_tree[i]);
                else
                    sb.Append("Null");
                if (i != _tree.Length - 1)
                    sb.Append(", ");
            }
            sb.Append("]");
            return sb.ToString();
        }

        public T Query(int query_l, int query_r)
        {
            if (query_l < 0 || query_l >= _data.Length 
                    || query_r < 0 || query_r >= _data.Length ||
                        query_l > query_r) /*Border Check*/
                throw new InvalidArgumentError($"Index ({query_l}, {query_r}) is invalid");
            return Query(0, 0, _data.Length - 1, query_l, query_r);
        }

        private T Query(int tree_index, int l, int r, int query_l, int query_r)
        {
            //在以tree_index为根节点的线段树中[l....r]的范围里，搜索区间[query_l...query_r]的值。
            if (l == query_l && r == query_r)
                return _tree[tree_index];

            int mid = l + (r - l) / 2;
            int left_tree_index = LeftChildOf(tree_index), 
                right_tree_index = RightChildOf(tree_index);
            if (query_l >= mid + 1)
                return Query(right_tree_index, mid + 1, r, query_l, query_r);
            else if (query_r <= mid)
                return Query(left_tree_index, l, mid, query_l, query_r);
            T left_res = Query(left_tree_index, l, mid, query_l, mid);
            T right_res = Query(right_tree_index, mid + 1, r, mid + 1, query_r);
            return Merge(left_res, right_res);

        }

        public static void Tester()
        {
            int?[] nums = { -2, 0, 3, -5, 2, -1 };

            SegmentTree<int?> segmentTree = new SegmentTree<int?>(
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
        /// <param name="new_ele"></param>        
        public void Updata(int index, T new_ele)
        {
            if (index < 0 || index >= _data.Length)
                throw new InvalidArgumentError($"Index:{index} is invalid.");
            _data[index] = new_ele;
            Update(0, 0, _data.Length - 1, index, new_ele);
            
        }

        private void Update(int tree_index, int l, int r, int index, T ele)
        {
            if (l == r)
            {
                _tree[tree_index] = ele;
                return;
            }
            int mid = l + (r - l) / 2;
            int left_tree_index = LeftChildOf(tree_index);
            int right_tree_index = RightChildOf(tree_index);
            if (index >= mid + 1)
                Update(right_tree_index, mid + 1, r, index, ele);
            else // index <= mid
                Update(left_tree_index, l, mid, index, ele);
            _tree[tree_index] = Merge(_tree[left_tree_index], _tree[right_tree_index]);
        }
    }
}
