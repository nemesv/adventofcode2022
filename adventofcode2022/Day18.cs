using System.Collections.Generic;

namespace adventofcode2022;

public class Day18 : Day
{
    public Day18(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var cubes = input.Split(Environment.NewLine).Select(l => l.Split(",").Select(int.Parse).ToArray()).ToArray();

        var result = 0;

        for (int i = 0; i < cubes.Length; i++)
        {
            var cube1 = cubes[i];
            var openSides = 6;
            for (int j = 0; j < cubes.Length; j++)
            {
                if (i == j)
                    continue;

                var cube2 = cubes[j];
                if (
                    Math.Abs(cube1[0] - cube2[0]) +
                    Math.Abs(cube1[1] - cube2[1]) +
                    Math.Abs(cube1[2] - cube2[2])
                    == 1
                    )
                    openSides--;
            }
            result += openSides;
        }

        return result.ToString();
    }

    private List<(int, int, int)> GetNeighbours((int, int, int) current)
    {
        var neighbours = new List<(int, int, int)>()
                {
                    (current.Item1, current.Item2, current.Item3 + 1),
                    (current.Item1, current.Item2, current.Item3 - 1),
                    (current.Item1, current.Item2 + 1, current.Item3),
                    (current.Item1, current.Item2 - 1, current.Item3),
                    (current.Item1 + 1, current.Item2, current.Item3),
                    (current.Item1 - 1, current.Item2, current.Item3),
                };
        return neighbours;
    }

    public override string Part2()
    {
        var cubes = input.Split(Environment.NewLine).Select(l => l.Split(",").Select(int.Parse).ToArray()).ToArray();

        var air = new HashSet<(int, int, int)>();
        var originalOpenSides = 0;
        var cubesSet = new HashSet<(int, int, int)>(cubes.Select(c => (c[0], c[1], c[2])));

        foreach (var cube in cubesSet)
        {
            foreach (var n in GetNeighbours(cube))
            {
                if (!cubesSet.Contains(n))
                {
                    originalOpenSides++;
                    air.Add(n);
                }
            }
        }
        var internalAir = new List<(int, int, int)>();
        var result = 0;
        foreach (var airCube in air)
        {
            var pocket = new Queue<(int, int, int)>() { };
            pocket.Enqueue(airCube);
            var visited = new HashSet<(int, int, int)>();

            while (true)
            {
                if (pocket.Count == 0)
                {
                    foreach (var item in visited)
                    {
                        internalAir.Add(item);
                    }
                    break;
                }
                var current = pocket.Dequeue();
                if (visited.Contains(current))
                    continue;

                visited.Add(current);
                var neighbours = GetNeighbours(current);
                foreach (var n in neighbours)
                {
                    if (cubesSet.Contains(n))
                        continue;

                    pocket.Enqueue(n);
                }
                if (visited.Count > air.Count)
                    break;
            }
        }
        foreach (var current in air)
        {
            if (internalAir.Contains(current))
                continue;

            var neighbours = GetNeighbours(current);
            foreach (var n in neighbours)
            {
                if (cubesSet.Contains(n))
                {
                    result++;
                }
            }
        }
        return result.ToString();
    }
}
