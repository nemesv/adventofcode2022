namespace adventofcode2022.tests;

public class Day14Tests : DayTest<Day14>
{
    [Theory]
    [InlineData("498,4 -> 498,6 -> 496,6\r\n503,4 -> 502,4 -> 502,9 -> 494,9", "24")]
    public void Part1(string input, string output)
    {
        var result = Sut(input).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("498,4 -> 498,6 -> 496,6\r\n503,4 -> 502,4 -> 502,9 -> 494,9", "93")]
    public void Part2(string input, string output)
    {
        var result = Sut(input).Part2();
        result.Should().Be(output);
    }
}
