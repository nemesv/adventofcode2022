namespace adventofcode2022.tests;

public class Day20Tests : DayTest<Day20>
{
    [Theory]
    [InlineData("1\r\n2\r\n-3\r\n3\r\n-2\r\n0\r\n4", "3")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("1\r\n2\r\n-3\r\n3\r\n-2\r\n0\r\n4", "1623178306")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
