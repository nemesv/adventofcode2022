using static System.Net.Mime.MediaTypeNames;

namespace adventofcode2022;

public class Day14 : Day
{
    public Day14(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var rocks = new HashSet<(int, int)>();
        foreach (var line in lines)
        {
            var parts = line.Split(" -> ");
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var from = parts[i].Split(",").Select(int.Parse).ToArray();
                var to = parts[i + 1].Split(",").Select(int.Parse).ToArray();

                for (int c = Math.Min(from[0], to[0]); c <= Math.Max(from[0], to[0]); c++)
                {
                    rocks.Add((c, from[1]));
                }
                for (int c = Math.Min(from[1], to[1]); c <= Math.Max(from[1], to[1]); c++)
                {
                    rocks.Add((from[0], c));
                }
            }
        }
        var maxDepth = rocks.Select(r => r.Item2).Max();
        var sand = new HashSet<(int, int)>();

        while (true)
        {
            var current = (500, 0);
            var fallOff = false;
            while (true)
            {
                var down = (current.Item1, current.Item2 + 1);
                var left = (current.Item1 - 1, current.Item2 + 1);
                var right = (current.Item1 + 1, current.Item2 + 1);
                if (!rocks.Contains(down) && !sand.Contains(down))
                {
                    current = down;
                }
                else if (!rocks.Contains(left) && !sand.Contains(left))
                {
                    current = left;
                }
                else if (!rocks.Contains(right) && !sand.Contains(right))
                {
                    current = right;
                }
                else
                {
                    sand.Add(current);
                    break;
                }
                if (current.Item2 > maxDepth)
                {
                    fallOff = true;
                    break;
                }
            }
            if (fallOff)
                break;
        }
        return sand.Count.ToString();
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine);
        var rocks = new HashSet<(int, int)>();
        foreach (var line in lines)
        {
            var parts = line.Split(" -> ");
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var from = parts[i].Split(",").Select(int.Parse).ToArray();
                var to = parts[i + 1].Split(",").Select(int.Parse).ToArray();

                for (int c = Math.Min(from[0], to[0]); c <= Math.Max(from[0], to[0]); c++)
                {
                    rocks.Add((c, from[1]));
                }
                for (int c = Math.Min(from[1], to[1]); c <= Math.Max(from[1], to[1]); c++)
                {
                    rocks.Add((from[0], c));
                }
            }
        }
        var maxDepth = rocks.Select(r => r.Item2).Max();
        var sand = new HashSet<(int, int)>();

        while (true)
        {
            var current = (500, 0);
            var fallOff = false;
            while (true)
            {
                var down = (current.Item1, current.Item2 + 1);
                var left = (current.Item1 - 1, current.Item2 + 1);
                var right = (current.Item1 + 1, current.Item2 + 1);
                if (!rocks.Contains(down) && !sand.Contains(down) && down.Item2 < maxDepth + 2)
                {
                    current = down;
                }
                else if (!rocks.Contains(left) && !sand.Contains(left) && down.Item2 < maxDepth + 2)
                {
                    current = left;
                }
                else if (!rocks.Contains(right) && !sand.Contains(right) && down.Item2 < maxDepth + 2)
                {
                    current = right;
                }
                else
                {
                    sand.Add(current);
                    break;
                }
            }
            if (sand.Contains((500, 0)))
                break;
        }
        return sand.Count.ToString();
    }
}
