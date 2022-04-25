using System;
using System.Collections.Generic;
using MuxLib.MUtility.Collections.Tree.Heap;

namespace MuxLib.MUtility.Encoding
{
    public class HuffmanTree
    {
        private class HuffmanNode : IComparable<HuffmanNode>, IComparable
        {
            public HuffmanNode? Left { get; set; } = null;
            public HuffmanNode? Right { get; set; } = null;
            public int Freq { get; set; } = 0;
            public char Symbol { set; get; }

            public HuffmanNode(char ch, int fre, HuffmanNode? left, HuffmanNode? right)
            {
                (Left, Right, Freq, Symbol) = (left, right, fre, ch);
            }

            public int CompareTo(HuffmanNode? node)
            {
                return Freq - node?.Freq ?? throw new Com.Errors.InvalidArgumentError();
            }

            public int CompareTo(object? obj)
            {
                return CompareTo((HuffmanNode?)obj);
            }
        }

        private HuffmanNode? Root { get; set; } = null;

        public static Dictionary<char, int> FrequencyStatic(string input)
        {
            Dictionary<char, int> ret = new Dictionary<char, int>();
            foreach (var c in input)
            {
                if (ret.ContainsKey(c)) ret[c]++;
                else ret[c] = 0;
            }

            return ret;
        }

        public Dictionary<char, string> EncodeTable { get; private set; } = new Dictionary<char, string>();
        public Dictionary<string, char> DecodeTable { get; private set; } = new Dictionary<string, char>();

        public void BuildMe(Dictionary<char, int> frequencyTable)
        {
            MaxHeap<HuffmanNode> heap = new MaxHeap<HuffmanNode>();
            foreach (var (key, value) in frequencyTable)
            {
                heap.Add(new HuffmanNode(key, value, null, null));
            }

            while (!heap.Empty)
            {
                HuffmanNode left = heap.ExtractMax();
                HuffmanNode right = heap.ExtractMax();

                var sum = left.Freq + right.Freq;
                heap.Add(new HuffmanNode('\0', sum, left, right));
            }

            Root = heap.ExtractMax();
            BuildEncodeTable(Root, "");
            BuildDecodeTable();
        }

        private void BuildEncodeTable(HuffmanNode? node, string str)
        {
            if (node == null) return;
            if (node.Left == null && node.Right == null)
            {
                EncodeTable[node.Symbol] = str;
            }

            BuildEncodeTable(node.Left, str + "0");
            BuildEncodeTable(node.Right, str + "1");
        }

        private void BuildDecodeTable()
        {
            foreach (var (key, value) in EncodeTable)
            {
                DecodeTable[value] = key;
            }
        }
    }
}