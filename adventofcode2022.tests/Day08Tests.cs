namespace adventofcode2022.tests;

public class Day08Tests : DayTest<Day08>
{
    [Theory]
    [InlineData("30373\r\n25512\r\n65332\r\n33549\r\n35390", "21")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("30373\r\n25512\r\n65332\r\n33549\r\n35390", "8")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
