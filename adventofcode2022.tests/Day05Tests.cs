namespace adventofcode2022.tests;

public class Day05Tests : DayTest<Day05>
{
    [Theory]
    [InlineData("    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2", "CMZ")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2", "MCD")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
