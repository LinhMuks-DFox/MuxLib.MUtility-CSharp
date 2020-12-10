using MuxLib.MUtility.Collections.Map;
namespace MuxLib.MUtility.Collections.Tree.Trie
{
    public sealed class Trie
    {
        private class Node
        {
            public bool IsWord { get; set; } = false;
            public AVLMap<char, Node> Next { set; get; } = new AVLMap<char, Node>();
            public Node(bool is_word) => IsWord = is_word;
            public Node() { }
        }

        private Node _root;
        private int _size;
        public Trie()
        {
            _root = new Node();
            _size = 0;
        }

        public int Size { get => _size; }

        public void Append(string word)
        {
            Node cur = _root;
            for (int i = 0; i < word.Length; ++i)
            {
                char c = word[i];
                if (cur.Next.Get(c) == null)
                    cur.Next.Set(c, new Node());
                cur = cur.Next.Get(c);
            }
            if (!cur.IsWord)
            {
                cur.IsWord = true;
                _size++;
            }
        }

        public bool Contains(string word)
        {
            Node cur = _root;
            for (int i = 0; i < word.Length; ++i)
            {
                char c = word[i];
                if (cur.Next.Get(c) == null)
                    return false;
                cur = cur.Next.Get(c);
            }
            return cur.IsWord;
        }

        public bool IsPrefix(string prefix)
        {
            Node cur = _root;
            for (int i = 0; i < prefix.Length; ++i)
            {
                char c = prefix[i];
                if (cur.Next.Get(c) == null)
                    return false;
                cur = cur.Next.Get(c);
            }
            return true;
        }


    }
}
