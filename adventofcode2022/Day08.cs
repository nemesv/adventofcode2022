using System.Linq;

namespace adventofcode2022;

public class Day08 : Day
{
    public Day08(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var trees = input.Split(Environment.NewLine).Select(l => l.ToArray()).ToArray();
        var visibles = new HashSet<(int, int)>();
        for (int row = 0; row < trees.Length; row++)
        {
            char max = '/';
            for (int column = 0; column < trees[0].Length; column++)
            {                
                if (column == 0 || trees[row][column] > max)
                {
                    visibles.Add((row, column));
                    if (trees[row][column] > max)
                        max = trees[row][column];
                }
            }
        }

        
        for (int column = 0; column < trees[0].Length; column++)
        {
            char max = '/';
            for (int row = 0; row < trees.Length; row++)
            {
                if (row == 0 || trees[row][column] > max)
                {
                    visibles.Add((row, column));
                    if (trees[row][column] > max)
                        max = trees[row][column];
                }
            }
        }

        for (int row = 0; row < trees.Length; row++)
        {
            char max = '/';
            for (int column = trees[0].Length - 1; column > 0; column--)
            {
                if (column == trees[0].Length - 1 || trees[row][column] > max)
                {
                    visibles.Add((row, column));
                    if (trees[row][column] > max)
                        max = trees[row][column];
                }
            }
        }

        for (int column = 0; column < trees[0].Length; column++)
        {
            char max = '/';
            for (int row = trees.Length - 1; row > 0; row--)
            {
                if (row == trees.Length - 1 || trees[row][column] > max)
                {
                    visibles.Add((row, column));
                    if (trees[row][column] > max)
                        max = trees[row][column];
                }
            }
        }
        //for (int i = 0; i < trees.Length; i++)
        //{
        //    for (int j = 0; j < trees[0].Length; j++)
        //    {
        //        if (visibles.Contains((i, j)))
        //            Console.Write(".");
        //        else
        //            Console.Write("X");

        //    }
        //    Console.WriteLine();
        //}
        return visibles.Count.ToString();
    }

    public override string Part2()
    {
        var trees = input.Split(Environment.NewLine).Select(l => l.ToArray()).ToArray();
        var visibles = new HashSet<(int, int)>();
        var max = 0;
        for (int row = 0; row < trees.Length; row++)
        {
            for(int column = 0; column < trees[0].Length; column++)
            {
                var current = trees[row][column];
                var lengthRight = 0;

                for (int right = column + 1; right < trees[0].Length; right++)
                {
                    if (trees[row][right] < current)
                        lengthRight++;
                    else
                    {
                        lengthRight++;
                        break;
                    }
                }
                var lengthLeft = 0;
                for (int left = column - 1; left >= 0; left--)
                {
                    if (trees[row][left] < current)
                        lengthLeft++;
                    else
                    {
                        lengthLeft++;
                        break;
                    }
                }
                var lengthDown = 0;
                for (int down = row + 1; down < trees.Length; down++)
                {
                    if (trees[down][column] < current)
                        lengthDown++;
                    else
                    {
                        lengthDown++;
                        break;
                    }
                }
                var lengthUp = 0;
                for (int up = row - 1; up >= 0; up--)
                {
                    if (trees[up][column] < current)
                        lengthUp++;
                    else
                    {
                        lengthUp++;
                        break;
                    }
                }

                var score = lengthRight * lengthLeft * lengthUp * lengthDown;
                if (score > max)
                    max = score;
            }
        }
        return max.ToString();
    }
}
