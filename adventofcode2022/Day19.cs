using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace adventofcode2022;

public class Day19 : Day
{
    public Day19(string input) : base(input)
    {
    }


    private int Search(int[][] costs, int maxTime)
    {

        var states = new Queue<(int[], int[], int)>();
        states.Enqueue((new[] { 1, 0, 0, 0 }, new[] { 0, 0, 0, 0 }, 0));
        var max = 0;
        var maxBots = new int[]
        {
            costs.Select(c => c[0]).Max(),
            costs.Select(c => c[1]).Max(),
            costs.Select(c => c[2]).Max(),
            int.MaxValue
        };
        while (states.Count > 0)
        {
            var state = states.Dequeue();
            var robots = state.Item1;
            var resources = state.Item2;
            var time = state.Item3;

            var geodes = resources[3] + robots[3] * (maxTime - time);
            time++;

            if (geodes > max)
                max = geodes;

            if (time > maxTime)
                continue;

            for (int i = 0; i < costs.Length; i++)
            {
                var cost = costs[i];
                if (robots[i] >= maxBots[i])
                    continue;

                var timeForNextBuild = 0d;
                for (int j = 0; j < costs.Length - 1; j++)
                {
                    var timeForResource = Math.Ceiling((cost[j] + 0.0d - resources[j]) / (robots[j]));
                    if (timeForResource > timeForNextBuild)
                        timeForNextBuild = timeForResource;
                }

                if (timeForNextBuild > maxTime - time)
                    continue;

                var nextResources = resources.Select((r, ind) => r + robots[ind] * ((int)timeForNextBuild + 1) - cost[ind]).ToArray();
                var nextRobots = robots.Select((r, ind) => ind == i ? r + 1 : r).ToArray();

                states.Enqueue((nextRobots, nextResources, time + (int)timeForNextBuild));
            }
        }
        return max;
    }

    public override string Part1()
    {
        var index = 1;
        var result = 0;
        foreach (var line in input.Split(Environment.NewLine))
        {
            var parts = Regex.Match(line, @"Blueprint \d+: Each ore robot costs (\d+) ore\. Each clay robot costs (\d+) ore\. Each obsidian robot costs (\d+) ore and (\d+) clay\. Each geode robot costs (\d+) ore and (\d+) obsidian\.");
            var costs = new int[][]
            {
                new int []{ int.Parse(parts.Groups[1].Value), 0, 0, 0 },
                new int []{ int.Parse(parts.Groups[2].Value), 0, 0, 0 },
                new int []{ int.Parse(parts.Groups[3].Value), int.Parse(parts.Groups[4].Value), 0, 0 },
                new int[]{ int.Parse(parts.Groups[5].Value), 0, int.Parse(parts.Groups[6].Value), 0 },
            };
            var max = Search(costs, 24);
            result += index * max;
            index++;
        }
        return result.ToString();
    }

    public override string Part2()
    {
        var index = 1;
        var results = new List<int>();
        foreach (var line in input.Split(Environment.NewLine).Take(3))
        {
            var parts = Regex.Match(line, @"Blueprint \d+: Each ore robot costs (\d+) ore\. Each clay robot costs (\d+) ore\. Each obsidian robot costs (\d+) ore and (\d+) clay\. Each geode robot costs (\d+) ore and (\d+) obsidian\.");
            var costs = new int[][]
            {
                new int []{ int.Parse(parts.Groups[1].Value), 0, 0, 0 },
                new int []{ int.Parse(parts.Groups[2].Value), 0, 0, 0 },
                new int []{ int.Parse(parts.Groups[3].Value), int.Parse(parts.Groups[4].Value), 0, 0 },
                new int[]{ int.Parse(parts.Groups[5].Value), 0, int.Parse(parts.Groups[6].Value), 0 },
            };
            var max = Search(costs, 32);
            results.Add(max);
        }
        return results.Aggregate(1, (acc, c) => acc * c).ToString();
    }
}
