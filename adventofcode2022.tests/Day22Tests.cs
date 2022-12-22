using System.Collections.Generic;

namespace adventofcode2022.tests;

public class Day22Tests : DayTest<Day22>
{
    [Theory]
    [InlineData("        ...#\r\n        .#..\r\n        #...\r\n        ....\r\n...#.......#\r\n........#...\r\n..#....#....\r\n..........#.\r\n        ...#....\r\n        .....#..\r\n        .#......\r\n        ......#.\r\n\r\n10R5L5R10L4R5L5", "6032")]
    public void Part1(string input, string output)
    {
        var result = Sut(input.Replace(",", "\r\n")).Part1();
        result.Should().Be(output);
    }

    [Theory]
    [InlineData("        ...#\r\n        .#..\r\n        #...\r\n        ....\r\n...#.......#\r\n........#...\r\n..#....#....\r\n..........#.\r\n        ...#....\r\n        .....#..\r\n        .#......\r\n        ......#.\r\n\r\n10R5L5R10L4R5L5", "5031")]
    public void Part2(string input, string output)
    {
        //               1(0,2)
        // 2(1,0) 3(1,1) 4(1,2)
        //               5(2,2) 6(2,3)

        // 1U -> 2U, 1L -> 3U, 1R -> 6R, 1D -> 4U        
        // 5U -> 4D, 5L -> 3D, 5R -> 6L, 5D -> 2D
        // 3R -> 4L, 4R -> 6U, 6D -> 2L, 2R -> 3L
        var sut = Sut(input.Replace(",", "\r\n"));
        sut.cube = new Dictionary<((int, int) cubePosition, (int, int) direction), ((int, int) cubePosition, (int, int) direction)>() {
            { ((0,2) , (-1,0)), ((1,0), (-1,0)) },
            { ((0,2) , (0, -1)), ((1,1), (-1,0))},
            { ((0,2) , (0, 1)), ((2,3), (0,1))},
            { ((0,2) , (1, 0)), ((1,2), (-1,0))},

            { ((2,2) , (-1,0)), ((1,2), (1,0)) },
            { ((2,2) , (0, -1)), ((1,1), (1,0))},
            { ((2,2) , (0, 1)), ((2,3), (0,-1))},
            { ((2,2) , (1, 0)), ((1,0), (1,0))},

            { ((1,1) , (0, 1)), ((1,2), (0,-1)) },
            { ((1,2) , (0, 1)), ((2,3), (-1,0))},
            { ((2,3) , (1, 0)), ((1,0), (0,-1))},
            { ((1,0) , (0, 1)), ((1,1), (0,-1))},
        };
        sut.cubeSize = 4;
        var result = sut.Part2();
        result.Should().Be(output);
    }
}
