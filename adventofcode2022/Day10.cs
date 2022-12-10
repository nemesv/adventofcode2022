namespace adventofcode2022;

public class Day10 : Day
{
    public Day10(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var values = GetXValues();
        var result = 0;
        var targetCycle = 19;
        for (int i = 0; i < 6; i++)
        {
            result += (targetCycle + 1) * values[targetCycle];
            targetCycle += 40;
        }
        return result.ToString();
    }

    public override string Part2()
    {
        var values = GetXValues();
        var offset = 0;
        for (int i = 0; i < values.Count; i++)
        {
            var current = values[i] + offset;
            if (i == current || i == current - 1 || i == current + 1)
            {
                Console.Write("X");
            }
            else
            {
                Console.Write(".");
            }

            if ((i + 1) % 40 == 0)
            {
                offset += 40;
                Console.WriteLine();
            }
        }
        return "";
    }

    private List<int> GetXValues()
    {
        var instructions = input.Split(Environment.NewLine);
        var x = 1;
        var results = new List<int>();
        foreach (var instruction in instructions)
        {
            var parts = instruction.Split(" ");
            var op = parts[0];
            switch (op)
            {
                case "noop":
                    results.Add(x);
                    break;
                case "addx":
                    var arg = int.Parse(parts[1]);
                    results.Add(x);
                    results.Add(x);
                    x += arg;
                    break;

                default:
                    break;
            }
        }
        return results;
    }
}
