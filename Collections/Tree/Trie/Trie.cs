using MuxLib.MUtility.Collections.Map;

namespace MuxLib.MUtility.Collections.Tree.Trie
{
    public sealed class Trie
    {
        private readonly Node _root;

        public Trie()
        {
            _root = new Node();
            Size = 0;
        }

        public int Size { get; private set; }

        public void Append(string word)
        {
            var cur = _root;
            for (var i = 0; i < word.Length; ++i)
            {
                var c = word[i];
                if (cur.Next.Get(c) == null)
                    cur.Next.Set(c, new Node());
                cur = cur.Next.Get(c);
            }

            if (!cur.IsWord)
            {
                cur.IsWord = true;
                Size++;
            }
        }

        public bool Contains(string word)
        {
            var cur = _root;
            for (var i = 0; i < word.Length; ++i)
            {
                var c = word[i];
                if (cur.Next.Get(c) == null)
                    return false;
                cur = cur.Next.Get(c);
            }

            return cur.IsWord;
        }

        public bool IsPrefix(string prefix)
        {
            var cur = _root;
            for (var i = 0; i < prefix.Length; ++i)
            {
                var c = prefix[i];
                if (cur.Next.Get(c) == null)
                    return false;
                cur = cur.Next.Get(c);
            }

            return true;
        }

        private class Node
        {
            public Node(bool is_word)
            {
                IsWord = is_word;
            }

            public Node()
            {
            }

            public bool IsWord { get; set; }
            public AVLMap<char, Node> Next { get; } = new();
        }
    }
}