namespace adventofcode2022;

public class Day02 : Day
{
    public Day02(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var result = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            switch (parts[0])
            {
                case "A": //1 rock
                    switch (parts[1])
                    {
                        //rock
                        case "X": //1  + 3
                            result += 4;
                            break;
                        //paper
                        case "Y": //2 + 6
                            result += 8;
                            break;
                        //scissors
                        case "Z": //3 + 0 
                            result += 3;
                            break;
                    }
                    break;
                case "B": //2 paper
                    switch (parts[1])
                    {
                        //rock
                        case "X": //1  + 0
                            result += 1;
                            break;
                        //paper
                        case "Y": //2  + 3
                            result += 5;
                            break;
                        //scissors
                        case "Z": //3  + 6
                            result += 9;
                            break;
                    }
                    break;
                case "C": //3 scissors
                    switch (parts[1])
                    {
                        //rock
                        case "X": //1  + 6
                            result += 7;
                            break;
                        //paper
                        case "Y": //2  + 0
                            result += 2;
                            break;
                        //scissors
                        case "Z": //3  + 3
                            result += 6;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        return result.ToString();
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine);
        var result = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            switch (parts[0])
            {
                case "A": //1 rock
                    switch (parts[1])
                    {
                        //lose
                        case "X": //3  + 0
                            result += 3;
                            break;
                        //draw
                        case "Y": //1 + 3
                            result += 4;
                            break;
                        //win
                        case "Z": //2 + 6 
                            result += 8;
                            break;
                    }
                    break;
                case "B": //2 paper
                    switch (parts[1])
                    {
                        //lose
                        case "X": //1  + 0
                            result += 1;
                            break;
                        //draw
                        case "Y": //2  + 3
                            result += 5;
                            break;
                        //win
                        case "Z": //3  + 6
                            result += 9;
                            break;
                    }
                    break;
                case "C": //3 scissors
                    switch (parts[1])
                    {
                        //lose
                        case "X": //2  + 0
                            result += 2;
                            break;
                        //draw
                        case "Y": //3  + 3
                            result += 6;
                            break;
                        //win
                        case "Z": //1  + 6
                            result += 7;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        return result.ToString();
    }
}
