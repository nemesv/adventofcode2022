using System.Text.RegularExpressions;
using System.Linq;
using System.Runtime.CompilerServices;

namespace adventofcode2022;

public class Day15 : Day
{
    public Day15(string input) : base(input)
    {
    }

    public override string Part1()
    {
        return Part1(2000000);
    }

    //06:30
    public string Part1(int targetY)
    {
        var lines = input.Split(Environment.NewLine);
        var spacesInRow = new HashSet<(int, int)>();
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"Sensor at x=(.*), y=(.*): closest beacon is at x=(.*), y=(.*)");
            var parsed = match.Groups.Cast<Group>().Skip(1).Select(m => int.Parse(m.Value)).ToArray();

            var distance = Math.Abs(parsed[0] - parsed[2]) + Math.Abs(parsed[1] - parsed[3]);

            if (Math.Abs(parsed[1] - targetY) <= distance)
            {
                var verticalDistance = Math.Abs(parsed[1] - targetY);
                for (int i = parsed[0] - distance + verticalDistance; i <= parsed[0] + distance - verticalDistance; i++)
                {
                    var r = Math.Abs(parsed[0] - i) + Math.Abs(parsed[1] - targetY);
                    if (r <= distance)
                    {
                        if ((i, targetY) != (parsed[2], parsed[3]))
                            spacesInRow.Add((i, targetY));
                    }
                }
            }
        }

        return spacesInRow.Count.ToString();
    }

    public override string Part2()
    {
        return Part2(4000000);
    }

    public string Part2(int max)
    {
        var lines = input.Split(Environment.NewLine);
        var spacesInRow = new HashSet<(long, long)>();
        var j = 0;
        var sensors = new List<(int x, int y, int d)>();
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"Sensor at x=(.*), y=(.*): closest beacon is at x=(.*), y=(.*)");
            var parsed = match.Groups.Cast<Group>().Skip(1).Select(m => int.Parse(m.Value)).ToArray();
            var distance = Math.Abs(parsed[0] - parsed[2]) + Math.Abs(parsed[1] - parsed[3]);
            sensors.Add((x: parsed[0], y: parsed[1], d: distance));
        }
        foreach (var sensor in sensors)
        {
            for (int y = sensor.y - (sensor.d + 1); y <= sensor.y + (sensor.d + 1); y++)
            {
                var d = Math.Abs(sensor.y - y);
                var endLeft = sensor.x - (sensor.d + 1) + d;
                var endRight = sensor.x + (sensor.d + 1) - d;
                if (0 <= endLeft && endLeft <= max && 0 <= y && y <= max)
                {
                    var canAdd = true;
                    foreach (var sensorInner in sensors)
                    {
                        if (Math.Abs(sensorInner.x - endLeft) + Math.Abs(sensorInner.y - y) <= sensorInner.d)
                        {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd)
                        spacesInRow.Add((endLeft, y));
                }
                if (0 <= endRight && endRight <= max && 0 <= y && y <= max)
                {
                    var canAdd = true;
                    foreach (var sensorInner in sensors)
                    {
                        if (Math.Abs(sensorInner.x - endRight) + Math.Abs(sensorInner.y - y) <= sensorInner.d)
                        {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd)
                        spacesInRow.Add((endRight, y));
                }
            }
        }
        var result = spacesInRow.Single();
        return (result.Item1 * 4000000 + result.Item2).ToString();
    }
}
