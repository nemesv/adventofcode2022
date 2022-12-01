namespace adventofcode2022;

public class Day01 : Day
{
    public Day01(string input) : base(input)
    {
    }

    private List<int> GetCalories()
    {
        var lines = input.Split(Environment.NewLine);
        var current = 0;
        var result = new List<int>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                result.Add(current);
                current = 0;
                continue;
            }
            current += int.Parse(line);            
        }
        if (current > 0)
            result.Add(current);
        return result;
    }

    public override string Part1()
    {
        return GetCalories().OrderByDescending(c => c).First().ToString();
    }    

    public override string Part2()
    {
        return GetCalories().OrderByDescending(c => c).Take(3).Sum().ToString();
    }
}
