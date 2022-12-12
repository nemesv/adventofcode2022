namespace adventofcode2022.tests;

public class Day12Tests : DayTest<Day12>
{
    [Theory]
    [InlineData("Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi", "31")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi", "29")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
