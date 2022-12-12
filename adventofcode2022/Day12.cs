namespace adventofcode2022;

public class Day12 : Day
{
    public Day12(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var map = input.Split(Environment.NewLine).Select(r => r.ToArray()).ToArray();
        var start = default((int, int));
        var end = default((int, int));
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] == 'S')
                {
                    start = (i, j);
                    map[i][j] = 'a';
                }
                if (map[i][j] == 'E')
                {
                    end = (i, j);
                    map[i][j] = 'z';
                }
            }
        }
        return GetDistance(start, end, map).ToString();
    }

    

    public override string Part2()
    {
        var map = input.Split(Environment.NewLine).Select(r => r.ToArray()).ToArray();
        var starts = new List<(int, int)>();
        var end = default((int, int));
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] == 'S')
                {
                    starts.Add((i, j));
                    map[i][j] = 'a';

                }
                if (map[i][j] == 'a')
                {
                    starts.Add((i, j));
                }
                if (map[i][j] == 'E')
                {
                    end = (i, j);
                    map[i][j] = 'z';
                }
            }
        }
        var min = int.MaxValue;
        foreach (var start in starts)
        {
            var d = GetDistance(start, end, map);
            if (d <= min)
            {
                min = d;
            }
        }
        return min.ToString();
    }

    private int GetDistance((int, int) start, (int, int) end, char[][] map)
    {
        var visited = new Dictionary<(int, int), int>();
        var queue = new Queue<(int, int)>();
        queue.Enqueue(start);
        visited.Add(start, 0);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var currentHeight = map[current.Item1][current.Item2];
            var currentDistance = visited[current];

            foreach (var item in new (int, int)[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
            {
                var nextRow = current.Item1 + item.Item1;
                var nextColumn = current.Item2 + item.Item2;
                var next = (nextRow, nextColumn);
                if (visited.ContainsKey(next))
                    continue;
                if (nextRow < 0 || nextRow > map.Length - 1 || nextColumn < 0 || nextColumn > map[0].Length - 1)
                    continue;
                var nextHeight = map[nextRow][nextColumn];
                if (nextHeight - currentHeight <= 1)
                {
                    queue.Enqueue((nextRow, nextColumn));
                    visited.Add(next, currentDistance + 1);
                }
            }
        }
        return visited.ContainsKey(end) ? visited[end] : int.MaxValue;
    }
}
