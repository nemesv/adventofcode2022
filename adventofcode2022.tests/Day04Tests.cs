namespace adventofcode2022.tests;

public class Day04Tests : DayTest<Day04>
{
    [Theory]
    [InlineData("2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8", "2")]
    public void Part1(string input, string output)
    {
        var result = Sut(input).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8", "4")]
    public void Part2(string input, string output)
    {
        var result = Sut(input).Part2();
        result.Should().Be(output);
    }
}
