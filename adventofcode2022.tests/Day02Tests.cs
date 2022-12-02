namespace adventofcode2022.tests;

public class Day02Tests : DayTest<Day02>
{
    [Theory]
    [InlineData("A Y\r\nB X\r\nC Z", "15")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("A Y\r\nB X\r\nC Z", "12")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
