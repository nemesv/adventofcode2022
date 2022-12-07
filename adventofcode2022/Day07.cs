namespace adventofcode2022;

public class Day07 : Day
{
    public Day07(string input) : base(input)
    {
    }

    public override string Part1()
    {
        return GetDirectories().Where(d => d.Value.GetTotalSize() <= 100000).Sum(d => d.Value.GetTotalSize()).ToString();
    }

    public Dictionary<string, Node> GetDirectories()
    {
        var root = new Node() { Name = "/" };
        var current = root;
        var directories = new Dictionary<string, Node>();
        directories["/"] = root;
        foreach (var line in input.Split(Environment.NewLine))
        {
            if (line.StartsWith("$ cd"))
            {
                var dir = line.Split(" ")[2];
                if (dir == "/")
                {
                    current = root;
                }
                else if (dir == "..")
                {
                    current = current.Parent;
                }
                else
                {
                    var next = current.Children.SingleOrDefault(n => n.Name == dir);
                    if (next == null)
                    {
                        next = new Node() { Name = dir, Path = $"{current.Path}/{dir}", Parent = current };
                        directories[next.Path] = next;
                    }
                    current.Children.Add(next);
                    current = next;
                }
            }
            else if (line.StartsWith("$ ls"))
            {
                continue;
            }
            else
            {
                var parts = line.Split(" ");
                if (parts[0] == "dir")
                {
                    continue;
                }
                else
                {
                    current.Children.Add(new Node() { Name = parts[1], Size = int.Parse(parts[0]) });
                }
            }
        }
        return directories;
    }

    public override string Part2()
    {
        var directories = GetDirectories();
        var totalSpace = 70000000;
        var freeSpaceNeeded = 30000000;
        var currentSpace = directories["/"].GetTotalSize();
        var currentFree = totalSpace - currentSpace;
        var deleteSize = freeSpaceNeeded - currentFree;
        var directoryToDelete = directories.Where(d => d.Value.GetTotalSize() >= deleteSize).OrderBy(d => d.Value.GetTotalSize()).First();

        return directoryToDelete.Value.GetTotalSize().ToString();
    }
}

public class Node
{
    public Node Parent { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public List<Node> Children { get; } = new List<Node>();

    public int? Size { get; set; }

    public int GetTotalSize()
    {
        var result = Size ?? 0;
        result += Children.Sum(c => c.GetTotalSize());
        return result;
    }
}
