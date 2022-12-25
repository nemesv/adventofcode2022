using System.Numerics;

namespace adventofcode2022;

public class Day25 : Day
{
    public Day25(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var numbers = input.Split(Environment.NewLine);
        var sum = 0L;
        foreach (var number in numbers)
        {
            sum += ConvertToDecimal(number);
        }
        return ConvertToSnafu(sum);
    }

    private long ConvertToDecimal(string snafu)
    {
        var result = 0L;
        for (int i = 0; i < snafu.Length; i++)
        {
            var currrent = snafu[snafu.Length - 1 - i];
            var mult = (long)Math.Pow(5, i);
            if (char.IsDigit(currrent))
            {
                result += mult * (int)char.GetNumericValue(currrent);
            }
            else
            {
                if (currrent == '-')
                {
                    result -= mult;
                }
                else
                    result -= 2 * mult;
            }
        }
        return result;
    }

    private string ConvertToSnafu(BigInteger number)
    {
        var baseFive = new List<int>();
        var result = "";
        while (number > 0)
        {
            baseFive.Add((int)(number % 5));
            number = number / 5;
        }
        while (baseFive.Count > 0)
        {
            var current = baseFive[0];
            baseFive.RemoveAt(0);
            if (current < 3)
            {
                result = current.ToString() + result;
            }
            else
            {
                result = (current == 3 ? "=" : "-") + result;
                if (baseFive.Count > 0)
                    baseFive[0] = baseFive[0] + 1;
                else
                    baseFive.Add(1);
            }
        }
        return result;
    }

    public override string Part2()
    {
        return "";
    }
}
