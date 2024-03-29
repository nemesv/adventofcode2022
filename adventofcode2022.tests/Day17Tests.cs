﻿namespace adventofcode2022.tests;

public class Day17Tests : DayTest<Day17>
{
    [Theory]
    [InlineData(">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", "3068")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData(">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", "1514285714288")]
    public void Part2(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part2();
        result.Should().Be(output);
    }
}
