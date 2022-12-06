namespace adventofcode2022;

public class Day06 : Day
{
    public Day06(string input) : base(input)
    {
    }

    public override string Part1()
    {
        return FindIndex(4).ToString();
    }

    public override string Part2()
    {
        return FindIndex(14).ToString();
    }

    int FindIndex(int length)
    {
        var index = 0;
        for (; index < input.Length - length; index++)
        {
            var current = input.Substring(index, length);
            var set = new HashSet<char>(current);
            if (set.Count == length)
                break;
        }

        return (index + length);
    }
}
