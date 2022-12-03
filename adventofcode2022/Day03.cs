namespace adventofcode2022;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class Day03 : Day
{
    public Day03(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var result = 0;
        foreach (var line in lines)
        {
            var first = line.Substring(0, line.Length / 2);
            var second = line.Substring(line.Length / 2);
            var common = first.Intersect(second);
            result += common.Select(c =>
            {
                if (char.IsAsciiLetterLower(c)) {
                    return c - 96;
                }
                else
                {
                    return c - 38;
                }
            }).Sum();
        }
        return result.ToString();
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine).ToList();
        var result = 0;
        foreach (var group in lines.Chunk(3))
        {
            var common = new HashSet<char>(group[0]).Intersect(new HashSet<char>(group[1])).Intersect(new HashSet<char>(group[2])).First();
            if (char.IsAsciiLetterLower(common))
            {
                result += common - 96;
            }
            else
            {
                result += common - 38;
            }
        }

        return result.ToString();
    }
}
