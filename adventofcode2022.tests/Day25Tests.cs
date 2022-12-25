namespace adventofcode2022.tests;

public class Day25Tests : DayTest<Day25>
{
    [Theory]
    [InlineData("1=-0-2\r\n12111\r\n2=0=\r\n21\r\n2=01\r\n111\r\n20012\r\n112\r\n1=-1=\r\n1-12\r\n12\r\n1=\r\n122", "2=-1=0")]
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
