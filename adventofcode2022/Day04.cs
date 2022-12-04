namespace adventofcode2022;

public class Day04 : Day
{
    public Day04(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var result = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var first = parts[0].Split("-").Select(int.Parse).ToArray();
            var second = parts[1].Split("-").Select(int.Parse).ToArray();
           
            if (first[0] <= second[0] && second[1] <= first[1] ||
                second[0] <= first[0] && first[1] <= second[1]
                )
            {
                result++;
            }
        }
        return result.ToString();
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine);
        var result = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var first = parts[0].Split("-").Select(int.Parse).ToArray();
            var second = parts[1].Split("-").Select(int.Parse).ToArray();

            if (first[0] <= second[0] && second[0] <= first[1] ||
                first[0] <= second[1] && second[1] <= first[1] ||
                second[0] <= first[0] && first[0] <= second[1] ||
                second[0] <= first[1] && first[1] <= second[1]
                )
            {
                result++;
            }
        }
        return result.ToString();
    }
}
