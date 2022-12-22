using System.Data;

namespace adventofcode2022;

public class Day22 : Day
{
    public Day22(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var instructions = lines[lines.Length - 1];
        var width = lines[..^2].Select(a => a.Length).Max();
        var grid = new Dictionary<(int row, int column), char>();
        for (int row = 0; row < lines.Length - 2; row++)
        {
            for (int column = 0; column < width; column++)
            {
                if (column < lines[row].Length)
                {
                    var current = lines[row][column];
                    if (current != ' ')
                        grid[(row, column)] = current;
                }
                else
                    break;
            }
        }

        var directions = new (int,int) [] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var direction = (0, 1);
        var currentPosition = grid.Where(g => g.Value == '.').Select(g => g.Key).OrderBy(k => k.row).ThenBy(k => k.column).First();
        var instructionPointer = 0;
        while (true)
        {
            var m = instructions.Skip(instructionPointer).TakeWhile(char.IsDigit).ToArray();
            instructionPointer += m.Length;
            var moves = int.Parse(string.Join("", m));
            for (int step = 0; step < moves; step++)
            {
                var nextPosition = (currentPosition.row + direction.Item1, currentPosition.column + direction.Item2);
                if (!grid.TryGetValue(nextPosition, out var gridCell))
                {
                    switch (direction)
                    {
                        case (0, 1): //right
                            nextPosition = grid.Keys.Where(k => k.row == currentPosition.row).OrderBy(k => k.column).First();
                            break;
                        case (0, -1): //left
                            nextPosition = grid.Keys.Where(k => k.row == currentPosition.row).OrderByDescending(k => k.column).First();
                            break;
                        case (1, 0): //down
                            nextPosition = grid.Keys.Where(k => k.column == currentPosition.column).OrderBy(k => k.row).First();
                            break;
                        case (-1, 0): //up
                            nextPosition = grid.Keys.Where(k => k.column == currentPosition.column).OrderByDescending(k => k.row).First();
                            break;
                        default:
                            break;
                    }
                    gridCell = grid[nextPosition];
                }
                if (gridCell == '#')
                    break;
                else
                {
                    currentPosition = nextPosition;
                }
            }
            if (instructionPointer == instructions.Length)
                break;
            var turn = instructions[instructionPointer];
            instructionPointer++;
            var directionIndex = Array.IndexOf(directions, direction);
            if (turn == 'R')
            {
                if (directionIndex + 1 == 4)
                    direction = directions[0];
                else
                    direction = directions[directionIndex + 1];

            }
            else
            {
                if (directionIndex - 1 == -1)
                    direction = directions[3];
                else
                    direction = directions[directionIndex - 1];
            }
        }
        return (1000 * (currentPosition.row + 1) + 4 * (currentPosition.column + 1) + Array.IndexOf(directions, direction)).ToString();
    }


    public int cubeSize = 50;
    //        1(0,1) 2(0,2)
    //        3(1,1) 
    // 4(2,0) 5(2,1) 
    // 6(3,0)

    // 1U -> 6L, 1L -> 4L, 1R -> 2L, 1D -> 3U        
    // 5U -> 3D, 5L -> 4R, 5R -> 2R, 5D -> 6R
    // 3R -> 2D, 2U -> 6D, 6U -> 4D, 4U -> 3L
    public Dictionary<((int, int) cubePosition, (int, int) direction), ((int, int) cubePosition, (int, int) direction)> cube = 
        new Dictionary<((int, int) cubePosition, (int, int) direction), ((int, int) cubePosition, (int, int) direction)>() {
            { ((0,1) , (-1,0)), ((3,0), (0,-1)) },
            { ((0,1) , (0, -1)), ((2,0), (0,-1))},
            { ((0,1) , (0, 1)), ((0,2), (0,-1))},
            { ((0,1) , (1, 0)), ((1,1), (-1,0))},

            { ((2,1) , (-1,0)), ((1,1), (1,0)) },
            { ((2,1) , (0, -1)), ((2,0), (0,1))},
            { ((2,1) , (0, 1)), ((0,2), (0,1))},
            { ((2,1) , (1, 0)), ((3,0), (0,1))},

            { ((1,1) , (0, 1)), ((0,2), (1,0)) },
            { ((0,2) , (-1, 0)), ((3,0), (1,0))},
            { ((3,0) , (-1, 0)), ((2,0), (1,0))},
            { ((2,0) , (-1, 0)), ((1,1), (0,-1))},
        };

    public override string Part2()
    {

        foreach (var c in cube.ToArray())
        {
            cube[c.Value] = c.Key;
        }
        var lines = input.Split(Environment.NewLine);
        var instructions = lines[lines.Length - 1];
        var width = lines[..^2].Select(a => a.Length).Max();
        var grid = new Dictionary<(int row, int column), char>();
        for (int row = 0; row < lines.Length - 2; row++)
        {
            for (int column = 0; column < width; column++)
            {
                if (column < lines[row].Length)
                {
                    var current = lines[row][column];
                    if (current != ' ')
                        grid[(row, column)] = current;
                }
                else
                    break;
            }
        }

        var directions = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var direction = (0, 1);
        var currentPosition = grid.Where(g => g.Value == '.').Select(g => g.Key).OrderBy(k => k.row).ThenBy(k => k.column).First();
        var instructionPointer = 0;

        while (true)
        {
            var m = instructions.Skip(instructionPointer).TakeWhile(char.IsDigit).ToArray();
            instructionPointer += m.Length;
            var cubePosition = (currentPosition.row / cubeSize, currentPosition.column / cubeSize);

            var moves = int.Parse(string.Join("", m));
            for (int step = 0; step < moves; step++)
            {
                var nextPosition = (currentPosition.row + direction.Item1, currentPosition.column + direction.Item2);
                var nextPositionInCube = (nextPosition.Item1 - cubePosition.Item1 * cubeSize, nextPosition.Item2 - cubePosition.Item2 * cubeSize);
                var nextDirection = direction;
                var gridCell = ' ';
                if (nextPositionInCube.Item1 <0 || nextPositionInCube.Item2 < 0 || nextPositionInCube.Item1 == cubeSize || nextPositionInCube.Item2 == cubeSize)
                {
                    var nextCubePosition = cube[(cubePosition, direction)];
                    switch (nextCubePosition.direction)
                    {
                        case (0, 1): //right
                            switch (direction)
                            {
                                case (0, 1): //right  
                                    nextPositionInCube = (cubeSize - nextPositionInCube.Item1 - 1, cubeSize - 1);
                                    break;
                                case (0, -1): //left                    
                                    nextPositionInCube = (nextPositionInCube.Item1, cubeSize - 1);
                                    break;
                                case (1, 0): //down                     
                                    nextPositionInCube = (nextPositionInCube.Item2, cubeSize - 1);
                                    break;
                                case (-1, 0): //up                      
                                    nextPositionInCube = (cubeSize - nextPositionInCube.Item2 - 1, cubeSize - 1);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case (0, -1): //left
                            switch (direction)
                            {
                                case (0, 1): //right  
                                    nextPositionInCube = (nextPositionInCube.Item1, 0);
                                    break;
                                case (0, -1): //left                    
                                    nextPositionInCube = (cubeSize - nextPositionInCube.Item1 - 1 , 0);
                                    break;
                                case (1, 0): //down                     
                                    nextPositionInCube = (cubeSize - nextPositionInCube.Item2 - 1, 0);
                                    break;
                                case (-1, 0): //up                      
                                    nextPositionInCube = (nextPositionInCube.Item2, 0);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case (1, 0): //down
                            switch (direction)
                            {
                                case (0, 1): //right  
                                    nextPositionInCube = (cubeSize - 1, nextPositionInCube.Item1);
                                    break;
                                case (0, -1): //left                    
                                    nextPositionInCube = (cubeSize - 1, cubeSize - nextPositionInCube.Item1 - 1);
                                    break;
                                case (1, 0): //down                     
                                    nextPositionInCube = (cubeSize - 1, cubeSize - nextPositionInCube.Item2 - 1);
                                    break;
                                case (-1, 0): //up                      
                                    nextPositionInCube = (cubeSize - 1, nextPositionInCube.Item2);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case (-1, 0): //up
                            switch (direction)
                            {
                                case (0, 1): //right  
                                    nextPositionInCube = (0, cubeSize - nextPositionInCube.Item1 - 1);
                                    break;
                                case (0, -1): //left
                                    nextPositionInCube = (0, nextPositionInCube.Item1);
                                    break;
                                case (1, 0): //down
                                    nextPositionInCube = (0, nextPositionInCube.Item2);
                                    break;
                                case (-1, 0): //up
                                    nextPositionInCube = (0, cubeSize - nextPositionInCube.Item2 - 1);
                                    break;
                                default:
                                    break;
                            };
                            break;
                        default:
                            break;
                    }
                    nextDirection = (nextCubePosition.direction.Item1 * -1, nextCubePosition.direction.Item2 * -1);
                    nextPosition = (nextPositionInCube.Item1 + nextCubePosition.cubePosition.Item1 * cubeSize, nextPositionInCube.Item2 + nextCubePosition.cubePosition.Item2 * cubeSize);
                    gridCell = grid[nextPosition];
                }                
                else
                {
                    gridCell = grid[nextPosition];
                }
                if (gridCell == '#')
                    break;
                else
                {
                    direction = nextDirection;
                    currentPosition = nextPosition;
                    cubePosition = (currentPosition.row / cubeSize, currentPosition.column / cubeSize);
                }
            }
            if (instructionPointer == instructions.Length)
                break;
            var turn = instructions[instructionPointer];
            instructionPointer++;
            var directionIndex = Array.IndexOf(directions, direction);
            if (turn == 'R')
            {
                if (directionIndex + 1 == 4)
                    direction = directions[0];
                else
                    direction = directions[directionIndex + 1];

            }
            else
            {
                if (directionIndex - 1 == -1)
                    direction = directions[3];
                else
                    direction = directions[directionIndex - 1];
            }
        }
        return (1000 * (currentPosition.row + 1) + 4 * (currentPosition.column + 1) + Array.IndexOf(directions, direction)).ToString();
    }
}
