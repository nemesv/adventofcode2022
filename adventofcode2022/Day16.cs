using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2022;

public class Day16 : Day
{
    public Day16(string input) : base(input)
    {
    }

    private Dictionary<string, List<string>> cache= new Dictionary<string, List<string>>();
    private List<string> GetPath(Dictionary<string, List<string>> graph, string start, string end)
    {
        if (cache.TryGetValue(start + end, out var list)) {
            return list;
        }
        var queue = new Queue<string>();
        var visited = new Dictionary<string, int>();
        var visitedFrom = new Dictionary<string, string>();
        queue.Enqueue(start);
        visited[start] = 1;
        while(queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var next in graph[current])
            {
                if (visited.TryGetValue(next, out var distance))
                {
                    if (distance > visited[current] + 1)
                    {
                        queue.Enqueue(next);
                        visited[next] = visited[current] + 1;
                        visitedFrom[next] = current;
                    }
                }
                else
                {
                    queue.Enqueue(next);
                    visited[next] = visited[current] + 1;
                    visitedFrom[next] = current;
                }
            }
        }

        var path = new List<string>() { end };
        var currentNode = end;
        while (true)
        {
            if (visitedFrom.TryGetValue(currentNode, out var prev))
            {
                path.Add(prev);
                currentNode = prev;
            }
            else
            {
                break;
            }
        }
        path.Reverse();
        cache[start + end] = path;
        return path;
    }

    public int? GetFinalScore(List<string> openOrder, Dictionary<string, int> flowRates, Dictionary<string, List<string>> graph)
    {
        var start = "AA";
        var flow = 0;
        var currentTime = 1;
        foreach (var open in openOrder)
        {
            var distance = GetPath(graph, start, open).Count - 1;
            currentTime += distance;
            currentTime += 1;
            if (currentTime >= 30)
                return null;
            flow += (30 - currentTime + 1) * flowRates[open];
            start = open;
        }
        return flow;
    }

    public void Search(Dictionary<string, int> flowRates, Dictionary<string, List<string>> graph, List<string> current, Dictionary<string, int> orderScore)
    {
        var score = GetFinalScore(current, flowRates, graph);
        if (score == null)
            return;
        orderScore[string.Join(",", current)] = score.Value;
        foreach (var next in flowRates.Where(p => p.Value > 0).Select(p => p.Key).Except(current))
        {
            Search(flowRates, graph, current.Concat(new[] {next} ).ToList(), orderScore);
        }
    }

    public override string Part1()
    {
        //20:55
        var graph = new Dictionary<string, List<string>>();
        var flowRates = new Dictionary<string, int>();
        foreach (var line in input.Split(Environment.NewLine))
        {
            var match = Regex.Match(line, "Valve (.*) has flow rate=(.*); tunnels? leads? to valves? (.*)");
            graph[match.Groups[1].Value] = match.Groups[3].Value.Split(",").Select(s => s.Trim()).ToList();
            flowRates[match.Groups[1].Value] = int.Parse(match.Groups[2].Value);
        }
        var orderScore = new Dictionary<string, int>() { { "", int.MaxValue } };
        var current = new List<string>();
        Search(flowRates, graph, new List<string>(), orderScore);

        return orderScore.Max(p => p.Value).ToString();
    }

    public override string Part2()
    {
        throw new System.NotImplementedException();
    }
}
