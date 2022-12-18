namespace adventofcode2022.tests;

public class Day18Tests : DayTest<Day18>
{
    [Theory]
    [InlineData("2,2,2\r\n1,2,2\r\n3,2,2\r\n2,1,2\r\n2,3,2\r\n2,2,1\r\n2,2,3\r\n2,2,4\r\n2,2,6\r\n1,2,5\r\n3,2,5\r\n2,1,5\r\n2,3,5", "64")]
    public void Part1(string input, string output)
    {
        var result = Sut(input).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("2,2,2\r\n1,2,2\r\n3,2,2\r\n2,1,2\r\n2,3,2\r\n2,2,1\r\n2,2,3\r\n2,2,4\r\n2,2,6\r\n1,2,5\r\n3,2,5\r\n2,1,5\r\n2,3,5", "58")]
    public void Part2(string input, string output)
    {
        var result = Sut(input).Part2();
        result.Should().Be(output);
    }
}
