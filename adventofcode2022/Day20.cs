using System.Diagnostics;
using System.Numerics;

namespace adventofcode2022;

public class Day20 : Day
{
    public Day20(string input) : base(input)
    {
    }

    public override string Part1()
    {
        return Solve(1, 1);
    }
    public override string Part2()
    {
        return Solve(10, 811589153L);
    }

    private string Solve(int turns, long multiplier)
    {
        var numbers = input.Split(Environment.NewLine).Select(int.Parse).ToList();
        var current = new Node() { Value = numbers[0] * multiplier };
        var nodes = new List<Node>() { current };
        var start = current;
        for (int i = 1; i < numbers.Count; i++)
        {
            var next = new Node() { Value = numbers[i] * multiplier };
            current.Next = next;
            next.Prev = current;
            current = next;
            nodes.Add(next);
        }
        var end = current;
        end.Next = start;
        start.Prev = end;

        for (int i = 0; i < turns; i++)
        {

            foreach (var node in nodes)
            {
                var currentNode = node;
                if (currentNode.Value > 0)
                {
                    var move = currentNode.Value % (nodes.Count - 1);
                    while (move > 0)
                    {
                        currentNode = node;

                        var toSwap = currentNode.Next;
                        var next = toSwap.Next;
                        var prev = currentNode.Prev;

                        if (currentNode == start)
                            start = toSwap;

                        prev.Next = toSwap;

                        toSwap.Prev = prev;
                        toSwap.Next = currentNode;

                        currentNode.Next = next;
                        currentNode.Prev = toSwap;

                        next.Prev = currentNode;

                        move--;
                    }
                }
                else
                {
                    var move = BigInteger.Abs(currentNode.Value) % (nodes.Count - 1);
                    while (move > 0)
                    {
                        currentNode = node;

                        var toSwap = currentNode;
                        currentNode = toSwap.Prev;

                        var next = toSwap.Next;
                        var prev = currentNode.Prev;

                        if (currentNode == start)
                            start = toSwap;

                        prev.Next = toSwap;

                        toSwap.Prev = prev;
                        toSwap.Next = currentNode;

                        currentNode.Next = next;
                        currentNode.Prev = toSwap;

                        next.Prev = currentNode;

                        if (toSwap == start)
                            start = currentNode;

                        move--;
                    }
                }
            }
        }
        var zero = nodes.Where(n => n.Value == 0).First();
        var resultNodes = new List<Node>();
        var s = zero;
        while (true)
        {
            resultNodes.Add(s);
            s = s.Next;
            if (s == zero)
                break;
        }
        var result = new BigInteger(0);
        foreach (var index in new[] { 1000, 2000, 3000 })
        {
            result += resultNodes[index % resultNodes.Count].Value;
        }

        return result.ToString();
    }

    public class Node
    {
        public BigInteger Value { get; set; }

        public Node Prev { get; set; }

        public Node Next { get; set; }

        public override string ToString()
        {
            return $"{Prev.Value} -> {Value} -> {Next.Value}";
        }
    }
}
