namespace adventofcode2022;

public class Day09 : Day
{
    public Day09(string input) : base(input)
    {
    }

    public override string Part1()
    {
        return NumberOfVisited(2).ToString();
    }

    public override string Part2()
    {
        return NumberOfVisited(10).ToString();
    }

    public int NumberOfVisited(int ropeLength)
    {
        var visited = new HashSet<(int row, int column)>();

        var rope = new List<(int row, int column)>();
        for (int i = 0; i < ropeLength; i++)
        {
            rope.Add((row: 0, column: 0));
        }
        visited.Add((row: 0, column: 0));
        foreach (var instructions in input.Split(Environment.NewLine))
        {
            var head = rope[0];
            var parts = instructions.Split(" ");
            var direction = parts[0];
            var steps = int.Parse(parts[1]);
            for (int i = 0; i < steps; i++)
            {
                switch (direction)
                {
                    case "R":
                        head = (head.row, head.column + 1);
                        break;
                    case "L":
                        head = (head.row, head.column - 1);
                        break;
                    case "U":
                        head = (head.row + 1, head.column);
                        break;
                    case "D":
                        head = (head.row - 1, head.column);
                        break;
                }
                rope[0] = head;
                for (int ropePart = 1; ropePart < rope.Count; ropePart++)
                {
                    var prev = rope[ropePart - 1];
                    var current = rope[ropePart];

                    var rowDiff = prev.row - current.row;
                    var columnDiff = prev.column - current.column;
                    if (Math.Abs(columnDiff) > 1 || Math.Abs(rowDiff) > 1)
                    {
                        current = (current.row + Math.Sign(rowDiff) * 1, current.column + Math.Sign(columnDiff) * 1);
                        rope[ropePart] = current;

                        if (ropePart == rope.Count - 1)
                            visited.Add(rope[ropePart]);
                    }
                }
            }
        }
        return visited.Count;
    }
}

