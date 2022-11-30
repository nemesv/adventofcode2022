namespace adventofcode2022.tests;

public class Day09Tests : DayTest<Day09>
{
    [Theory]
    [InlineData("input", "expected")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("input", "expected")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
