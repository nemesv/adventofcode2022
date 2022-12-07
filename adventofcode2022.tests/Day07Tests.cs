namespace adventofcode2022.tests;

public class Day07Tests : DayTest<Day07>
{
    [Theory]
    [InlineData("$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k", "95437")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k", "24933642")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
