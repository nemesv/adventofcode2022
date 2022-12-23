namespace adventofcode2022.tests;

public class Day23Tests : DayTest<Day23>
{
    [Theory]
    [InlineData("..............\r\n..............\r\n.......#......\r\n.....###.#....\r\n...#...#.#....\r\n....#...##....\r\n...#.###......\r\n...##.#.##....\r\n....#..#......\r\n..............\r\n..............\r\n..............\r\n", "110")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("..............\r\n..............\r\n.......#......\r\n.....###.#....\r\n...#...#.#....\r\n....#...##....\r\n...#.###......\r\n...##.#.##....\r\n....#..#......\r\n..............\r\n..............\r\n..............\r\n", "20")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
