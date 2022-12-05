using System.Text.RegularExpressions;

namespace adventofcode2022;

public class Day05 : Day
{
    public Day05(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var isConfig = true;
        var numberOfStacks = (lines[0].Length +1) / 4;
        var stacks = new List<List<char>>();
        for (int i = 0; i < numberOfStacks; i++)
        {
            stacks.Add(new List<char>());
        }
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                foreach (var stack in stacks)
                {
                    stack.Reverse();
                }
                isConfig = false;
                continue;
            }
            if (isConfig)
            {
                for (int i = 0; i < numberOfStacks; i++)
                {
                    var part = line.Substring(i * 4, i == numberOfStacks - 1 ? 3 : 4);
                    if (part[0] == '[')
                    {
                        stacks[i].Add(part[1]);
                    }
                }
            }
            else
            {
                var parts = Regex.Match(line, @"move (\d+) from (\d+) to (\d+)");
                var number = int.Parse(parts.Groups[1].Value);
                var from = int.Parse(parts.Groups[2].Value) - 1;
                var to = int.Parse(parts.Groups[3].Value) - 1;
                for (int i = 0; i < number; i++)
                {
                    var last = stacks[from].Last();
                    stacks[from].RemoveAt(stacks[from].Count - 1);
                    stacks[to].Add(last);
                }
            }
        }
        return string.Join("", stacks.Select(s => s.Last()));
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine);
        var isConfig = true;
        var numberOfStacks = (lines[0].Length + 1) / 4;
        var stacks = new List<List<char>>();
        for (int i = 0; i < numberOfStacks; i++)
        {
            stacks.Add(new List<char>());
        }
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                foreach (var stack in stacks)
                {
                    stack.Reverse();
                }
                isConfig = false;
                continue;
            }
            if (isConfig)
            {
                for (int i = 0; i < numberOfStacks; i++)
                {
                    var part = line.Substring(i * 4, i == numberOfStacks - 1 ? 3 : 4);
                    if (part[0] == '[')
                    {
                        stacks[i].Add(part[1]);
                    }
                }
            }
            else
            {
                var parts = Regex.Match(line, @"move (\d+) from (\d+) to (\d+)");
                var number = int.Parse(parts.Groups[1].Value);
                var from = int.Parse(parts.Groups[2].Value) - 1;
                var to = int.Parse(parts.Groups[3].Value) - 1;
                var last = stacks[from].TakeLast(number).ToArray();
                for (int i = 0; i < number; i++)
                {
                    stacks[from].RemoveAt(stacks[from].Count - 1);
                }
                stacks[to].AddRange(last);
            }
        }
        return string.Join("", stacks.Select(s => s.Last()));
    }
}
