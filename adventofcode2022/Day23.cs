namespace adventofcode2022;

public class Day23 : Day
{
    public Day23(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var elves = new HashSet<(int row, int column)>();
        for (int row = 0; row < lines.Length; row++)
        {
            for (int column = 0; column < lines[row].Length; column++)
            {
                if (lines[row][column] == '#')
                    elves.Add((row, column));
            }
        }
        var numberOfRounds = 10;
        var directions = new List<string> { "N", "S", "W", "E" };
        var mainDirection = 0;
        for (int round = 0; round < numberOfRounds; round++)
        {
            if (SimulateRound(elves, mainDirection, directions))
                mainDirection = (mainDirection + 1) % 4;
            else
                break;
        }

        var height = Math.Abs(elves.Max(e => e.row) - elves.Min(e => e.row) + 1);
        var width = Math.Abs(elves.Max(e => e.column) - elves.Min(e => e.column) + 1);

        return ((height * width) - elves.Count).ToString();
    }

    private bool SimulateRound(HashSet<(int row, int column)> elves, int mainDirection, List<string> directions)
    {
        var possibleMoves = new List<((int row, int column), (int row, int column))>();
        foreach (var elf in elves)
        {
            var directionToMove = new List<string>();
            if (!elves.Contains((elf.row - 1, elf.column)) && !elves.Contains((elf.row - 1, elf.column - 1)) && !elves.Contains((elf.row - 1, elf.column + 1)))
                directionToMove.Add("N");

            if (!elves.Contains((elf.row + 1, elf.column)) && !elves.Contains((elf.row + 1, elf.column - 1)) && !elves.Contains((elf.row + 1, elf.column + 1)))
                directionToMove.Add("S");

            if (!elves.Contains((elf.row + 1, elf.column + 1)) && !elves.Contains((elf.row, elf.column + 1)) && !elves.Contains((elf.row - 1, elf.column + 1)))
                directionToMove.Add("E");

            if (!elves.Contains((elf.row + 1, elf.column - 1)) && !elves.Contains((elf.row, elf.column - 1)) && !elves.Contains((elf.row - 1, elf.column - 1)))
                directionToMove.Add("W");

            if (directionToMove.Count == 4)
            {
                possibleMoves.Add((elf, elf));
                continue;
            }

            for (int directionIndex = 0; directionIndex < 4; directionIndex++)
            {
                var directionToCheck = directions[(mainDirection + directionIndex) % 4];
                if (directionToMove.Contains(directionToCheck))
                {
                    switch (directionToCheck)
                    {
                        case "N":
                            possibleMoves.Add((elf, (elf.row - 1, elf.column)));
                            break;
                        case "S":
                            possibleMoves.Add((elf, (elf.row + 1, elf.column)));
                            break;
                        case "W":
                            possibleMoves.Add((elf, (elf.row, elf.column - 1)));
                            break;
                        case "E":
                            possibleMoves.Add((elf, (elf.row, elf.column + 1)));
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }
        }

        if (possibleMoves.Count(m => m.Item1 != m.Item2) == 0)
            return false;

        foreach (var move in possibleMoves.GroupBy(g => g.Item2).Where(g => g.Count() == 1))
        {
            elves.Remove(move.First().Item1);
            elves.Add(move.First().Item2);
        }
        return true;
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine);
        var elves = new HashSet<(int row, int column)>();
        for (int row = 0; row < lines.Length; row++)
        {
            for (int column = 0; column < lines[row].Length; column++)
            {
                if (lines[row][column] == '#')
                    elves.Add((row, column));
            }
        }
        var directions = new List<string> { "N", "S", "W", "E" };
        var mainDirection = 0;
        var round = 1;
        while(true)
        {

            if (SimulateRound(elves, mainDirection, directions))
                mainDirection = (mainDirection + 1) % 4;
            else
                break;

            round++;
        }


        return round.ToString();
    }
}
