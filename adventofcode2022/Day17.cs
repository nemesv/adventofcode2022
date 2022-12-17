namespace adventofcode2022;

public class Day17 : Day
{
    public Day17(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var shapes = new List<List<(int, int)>> {
            new List<(int, int)> { (0,0), (0,1), (0,2), (0, 3) },
            new List<(int, int)> { (2,1), (1,0), (1,1), (1, 2), (0, 1) },
            new List<(int, int)> { (2,2), (1,2), (0,0), (0, 1), (0, 2) },
            new List<(int, int)> { (0,0), (1,0), (2,0), (3, 0) },
            new List<(int, int)> { (0,0), (0,1), (1,0), (1, 1) },
        };

        var jetPatterns = input.ToArray();
        var occupiedSpaces = new HashSet<(int,int)>() { (-1,1), (-1, 2), (-1, 3), (-1, 4), (-1, 5), (-1, 6) };
        var currentHeight = 0;
        var currentPattern = 0;
        for (int i = 0; i < 2022; i++)
        {            
            var nextShape = shapes[i % shapes.Count];
            var startHeight = currentHeight + 3;
            var startWidth = 2;
            var position = nextShape.Select(s => (s.Item1 + startHeight, s.Item2 + startWidth)).ToList();
            while (true)
            {
                var jet = jetPatterns[currentPattern];
                currentPattern++;
                if (currentPattern == jetPatterns.Length)
                    currentPattern = 0;

                var jetOffset = jet == '>' ? 1 : -1;
                var newPosition = position.Select(s => (s.Item1, s.Item2 + jetOffset)).ToList();
                if (newPosition.All(p => 0 <= p.Item2 && p.Item2 < 7) && !newPosition.Any(occupiedSpaces.Contains))
                    position = newPosition;
                var downOffset = -1;
                newPosition = position.Select(s => (s.Item1 + downOffset, s.Item2)).ToList();
                if (newPosition.Any(occupiedSpaces.Contains)) {
                    foreach (var s in position)
                    {
                        occupiedSpaces.Add(s);
                    }
                    break;
                }
                else
                {
                    position = newPosition;
                }
            }
            currentHeight = occupiedSpaces.Max(o => o.Item1) + 1;
        }

        return currentHeight.ToString();
    }

    public override string Part2()
    {
        var shapes = new List<List<(int, int)>> {
            new List<(int, int)> { (0,0), (0,1), (0,2), (0, 3) },
            new List<(int, int)> { (2,1), (1,0), (1,1), (1, 2), (0, 1) },
            new List<(int, int)> { (2,2), (1,2), (0,0), (0, 1), (0, 2) },
            new List<(int, int)> { (0,0), (1,0), (2,0), (3, 0) },
            new List<(int, int)> { (0,0), (0,1), (1,0), (1, 1) },
        };

        var jetPatterns = input.ToArray();
        var occupiedSpaces = new HashSet<(int, int)>() { (-1, 0), (-1, 1), (-1, 2), (-1, 3), (-1, 4), (-1, 5), (-1, 6) };
        var currentHeight = 0;
        var currentPattern = 0;
        var index = new List<string>();
        var visitedList = new List<(string, int)>();
        var firstRepeat = "";
        for (int i = 0; i < 2022; i++)
        {
            var nextShape = shapes[i % shapes.Count];
            var startHeight = currentHeight + 3;
            var startWidth = 2;
            var position = nextShape.Select(s => (s.Item1 + startHeight, s.Item2 + startWidth)).ToList();
            while (true)
            {
                var jet = jetPatterns[currentPattern];
                currentPattern++;
                if (currentPattern == jetPatterns.Length)
                    currentPattern = 0;
                var jetOffset = jet == '>' ? 1 : -1;
                var newPosition = position.Select(s => (s.Item1, s.Item2 + jetOffset)).ToList();
                if (newPosition.All(p => 0 <= p.Item2 && p.Item2 < 7) && !newPosition.Any(occupiedSpaces.Contains))
                    position = newPosition;
                var downOffset = -1;
                newPosition = position.Select(s => (s.Item1 + downOffset, s.Item2)).ToList();
                if (newPosition.Any(occupiedSpaces.Contains))
                {
                    foreach (var s in position)
                    {
                        occupiedSpaces.Add(s);
                    }
                    currentHeight = occupiedSpaces.Max(o => o.Item1) + 1;
                    var add = $"{i % shapes.Count} - {currentPattern - 1} - ${string.Join(", ", Enumerable.Range(0, 6).Select(i => occupiedSpaces.Where(s => s.Item2 == i).Max(o => o.Item1) - currentHeight))}";
                    if (!visitedList.Any(v => v.Item1 == add))
                    {
                        visitedList.Add((add, currentHeight));
                    }
                    else
                    {
                        if (firstRepeat == "")
                            firstRepeat = add;
                    }
                    index.Add(currentHeight.ToString());
                    break;
                }
                else
                {
                    position = newPosition;
                }
            }
            
        }
        var firstRepeatItem = visitedList.Single(v => v.Item1 == firstRepeat);
        var repeatIndex = visitedList.IndexOf(firstRepeatItem);
        var cycleLength = (visitedList.Count - repeatIndex);
        var cycles = (1000000000000 - visitedList.Count) / cycleLength;
        var remainder = (int)((1000000000000 - visitedList.Count) % cycleLength);

        var init = visitedList.Last().Item2;
        var c = (cycles * (visitedList.Last().Item2 - visitedList[repeatIndex - 1].Item2));
        var r = (visitedList[repeatIndex + remainder - 1].Item2 - visitedList[repeatIndex - 1].Item2);
        return (init + c + r).ToString();
    }
}
