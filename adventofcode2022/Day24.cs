using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace adventofcode2022;

public class Day24 : Day
{
    public Day24(string input) : base(input)
    {
    }

    private List<(int row, int column, char item)> GetNewGrid(List<(int row, int column, char item)> grid, int height, int width)
    {
        var newGrid = new List<(int row, int column, char item)>();
        foreach (var item in grid)
        {
            if (item.item == '#')
            {
                newGrid.Add(item);
            }
            else
            {
                switch (item.item)
                {
                    case '>':
                        if (item.column + 1 <= width - 2)
                            newGrid.Add((item.row, item.column + 1, item.item));
                        else
                            newGrid.Add((item.row, 1, item.item));
                        break;
                    case '<':
                        if (item.column - 1 >= 1)
                            newGrid.Add((item.row, item.column - 1, item.item));
                        else
                            newGrid.Add((item.row, width - 2, item.item));
                        break;
                    case 'v':
                        if (item.row + 1 <= height - 2)
                            newGrid.Add((item.row + 1, item.column, item.item));
                        else
                            newGrid.Add((1, item.column, item.item));
                        break;
                    case '^':
                        if (item.row - 1 >= 1)
                            newGrid.Add((item.row - 1, item.column, item.item));
                        else
                            newGrid.Add((height - 2, item.column, item.item));
                        break;
                    default:
                        break;
                }
            }
        }
        return newGrid;
    }

    public List<(int, int)> GetPosibbleMoves((int row, int column) current, List<(int row, int column, char item)> newGrid, int height, int width, (int, int)[] directions)
    {
        var possibleSteps = new List<(int, int)>();
        foreach (var direction in directions)
        {
            var nextStep = (current.row + direction.Item1, current.column + direction.Item2);
            if (nextStep.Item1 < 0 || nextStep.Item2 < 0 || nextStep.Item2 >= width || nextStep.Item1 >= height)
                continue;

            if (newGrid.Any(g => g.row == nextStep.Item1 && g.column == nextStep.Item2))
                continue;
            possibleSteps.Add(nextStep);
        }

        if (!newGrid.Any(g => g.row == current.Item1 && g.column == current.Item2))
        {
            possibleSteps.Add(current);
        }

        if (possibleSteps.Count == 0)
        {
            return null;
        }

        return possibleSteps;
    }

    public string GetHashKey((int, int) position, int grindIndex)
    {
        return $"{position} - {grindIndex}";
    }

    private void Print((int, int) position, List<(int row, int column, char item)> grid, int height, int width)
    {
        Console.WriteLine("---");
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var c = (i, j);
                if (position == c)
                {
                    Console.Write("E");
                    continue;
                }
                var items = grid.Where(g => g.row == i && g.column == j).ToList();
                if (items.Count > 1)
                {
                    Console.Write(items.Count);
                    continue;
                }
                if (items.Count == 1)
                {
                    Console.Write(items[0].item);
                    continue;
                }
                Console.Write(" ");
            }
            Console.WriteLine("");
        }
    }

    private (int steps, int gridIndex) GetPath((int,int) start, (int,int) end, int startIndex, List<List<(int row, int column, char item)>> grids, int height, int width)
    {
        var queue = new Queue<((int, int) position, int gridIndex, int distance)>();
        queue.Enqueue((start, startIndex, 0));
        var visited = new HashSet<string>();
        var directions = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var result = (steps: int.MaxValue, gridIndex: 0);

        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            var cHash = GetHashKey(c.position, c.gridIndex);
            if (visited.Contains(cHash))
                continue;
            visited.Add(cHash);

            if (c.distance >= result.steps)
                continue;

            if (c.position.Item1 == end.Item1 && c.position.Item2 == end.Item2)
            {
                if (c.distance < result.steps)
                {
                    result = (c.distance, c.gridIndex);
                }
                continue;
            }

            var current = c.position;
            var nextGridIndex = (c.gridIndex + 1) % grids.Count;
            var nextGrid = grids[nextGridIndex];
            var possibleMoves = GetPosibbleMoves(current, nextGrid, height, width, directions);
            if (possibleMoves != null)
            {
                foreach (var move in possibleMoves)
                {
                    var next = (move, nextGridIndex, c.distance + 1);
                    var nextHash = GetHashKey(next.move, nextGridIndex);
                    if (!visited.Contains(nextHash))
                        queue.Enqueue(next);
                }
            }
        }

        return result;
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var grid = new List<(int row, int column, char item)>();
        for (int row = 0; row < lines.Length; row++)
        {
            for (int column = 0; column < lines[0].Length; column++)
            {
                var c = lines[row][column];
                if (c != '.')
                {
                    grid.Add((row, column, c));
                }
            }
        }
        var width = lines[0].Length;
        var height = lines.Length;
        var grids = new List<List<(int row, int column, char item)>>();
        var visistedGrids = new HashSet<string>();
        var currentGrid = grid;
        while (true)
        {
            var hashKey = string.Join("|", currentGrid);
            if (visistedGrids.Contains(hashKey))
                break;

            visistedGrids.Add(hashKey);
            grids.Add(currentGrid);
            currentGrid = GetNewGrid(currentGrid, height, width);
        }

        var result = GetPath((0, 1), (height - 1, width - 2), 0 ,grids, height, width);
        return result.steps.ToString();
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine);
        var grid = new List<(int row, int column, char item)>();
        for (int row = 0; row < lines.Length; row++)
        {
            for (int column = 0; column < lines[0].Length; column++)
            {
                var c = lines[row][column];
                if (c != '.')
                {
                    grid.Add((row, column, c));
                }
            }
        }
        var width = lines[0].Length;
        var height = lines.Length;
        var grids = new List<List<(int row, int column, char item)>>();
        var visistedGrids = new HashSet<string>();
        var currentGrid = grid;
        while (true)
        {
            var hashKey = string.Join("|", currentGrid);
            if (visistedGrids.Contains(hashKey))
                break;

            visistedGrids.Add(hashKey);
            grids.Add(currentGrid);
            currentGrid = GetNewGrid(currentGrid, height, width);
        }

        var firstTrip = GetPath((0, 1), (height - 1, width - 2), 0, grids,  height, width);
        var backTrip = GetPath((height - 1, width - 2), (0, 1), firstTrip.gridIndex, grids, height, width);
        var secondTrip = GetPath((0, 1), (height - 1, width - 2), backTrip.gridIndex, grids, height, width);
        return (firstTrip.steps + backTrip.steps + secondTrip.steps).ToString();
    }
}
