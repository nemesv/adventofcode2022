using System.Text.RegularExpressions;

namespace adventofcode2022;

public class Day16 : Day
{
    public Day16(string input) : base(input)
    {
    }

    private Dictionary<string, List<string>> cache = new Dictionary<string, List<string>>();
    private List<string> GetPath(Dictionary<string, List<string>> graph, string start, string end)
    {
        if (cache.TryGetValue(start + end, out var list))
        {
            return list;
        }
        var queue = new Queue<string>();
        var visited = new Dictionary<string, int>();
        var visitedFrom = new Dictionary<string, string>();
        queue.Enqueue(start);
        visited[start] = 1;
        while (queue.Count > 0)
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

    private List<string> positiveFlowValves;
    private int timeLimit;

    private Dictionary<string, (int?, int)> scoreCache = new Dictionary<string, (int?, int)>();
    public (int?, int) GetFinalScore(string openOrder, Dictionary<string, int> flowRates, Dictionary<string, List<string>> graph)
    {
        var cacheKey = openOrder;
        if (scoreCache.TryGetValue(cacheKey, out var score))
        {
            return score;
        }

        var start = "AA";
        var flow = 0;
        var currentTime = 1;
        foreach (var open in openOrder.Split(","))
        {
            if (open == "")
                continue;
            var distance = GetPath(graph, start, open).Count - 1;
            currentTime += distance;
            currentTime += 1;
            if (currentTime > timeLimit)
            {
                scoreCache[cacheKey] = (null, currentTime);
                return (null, currentTime);
            }
            flow += (timeLimit - currentTime +1) * flowRates[open];
            start = open;
        }
        scoreCache[cacheKey] = (flow, currentTime);
        return (flow, currentTime);
    }

    public void Search(Dictionary<string, int> flowRates,
        Dictionary<string, List<string>> graph,
        string current,
        Dictionary<string, int> orderScore)
    {
        var score = GetFinalScore(current, flowRates, graph).Item1;
        if (score == null)
            return;

        orderScore[string.Join(",", current)] = score.Value;
        if (score.Value > max)
        {
            max = score.Value;
            Console.WriteLine($"{max} - {current}");
        }
        foreach (var next in positiveFlowValves)
        {
            if (current.Contains(next))
                continue;

            Search(flowRates, graph, current + "," + next, orderScore);
        }
    }

    private int max = 0;
    public void Search(Dictionary<string, int> flowRates,
    Dictionary<string, List<string>> graph,
    string current,
    string elephant,
    Dictionary<string, int> orderScore)
    {
        if (orderScore.ContainsKey(current + "-" + elephant))
        {            
            return;
        }
        if (orderScore.ContainsKey(elephant + "-" + current))
        {
            return;
        }
        var score = GetFinalScore(current, flowRates, graph);
        if (score.Item1 == null)
            return;
        var scoreElephant = GetFinalScore(elephant, flowRates, graph);
        if (scoreElephant.Item1 == null)
            return;

        var newScore = score.Item1.Value + scoreElephant.Item1.Value;

        var idealMaxScore = newScore;
        foreach (var v in positiveFlowValves)
        {
            if (current.Contains(v) || elephant.Contains(v)) continue;
            idealMaxScore += flowRates[v] * (timeLimit - Math.Min(score.Item2, scoreElephant.Item2));
        }
        if (idealMaxScore < max)
        {
            return;
        }

        orderScore[current + "-" + elephant] = newScore;
        if (newScore > max)
        {
            max = newScore;
            Console.WriteLine($"{max} - {current} - {elephant} - {score.Item2} - {scoreElephant.Item2}");
        }
                
        foreach (var next in positiveFlowValves)
        {
            if (current.Contains(next) || elephant.Contains(next)) continue;

            var nextCurrent = current + "," + next;
            var foundElephant = false;
            Search(flowRates, graph, nextCurrent, elephant, orderScore);
            foreach (var nextElephant in positiveFlowValves)
            {
                if (nextCurrent.Contains(nextElephant) || elephant.Contains(nextElephant)) continue;
                foundElephant = true;
                Search(flowRates, graph, nextCurrent, elephant + "," + nextElephant, orderScore);
            }
        }
    }

    public override string Part1()
    {
        //20:55 - 24:00
        //07:30 - 08:45
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
        positiveFlowValves = flowRates.Where(p => p.Value > 0).Select(p => p.Key).ToList();

        timeLimit = 30;
        Search(flowRates, graph, "", orderScore);
        var max1 = orderScore.OrderByDescending(p => p.Value).ToList();
        return orderScore.Max(p => p.Value).ToString();
    }

    public override string Part2()
    {
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
        positiveFlowValves = flowRates.Where(p => p.Value > 0).Select(p => p.Key).ToList();
        timeLimit = 26;
        max = 0;
        scoreCache = new Dictionary<string, (int?, int)>();
        Search(flowRates, graph, "", "", orderScore);
        var max1 = orderScore.OrderByDescending(p => p.Value).ToList();
        return max.ToString();
    }
}
