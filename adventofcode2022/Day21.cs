using System.Text.RegularExpressions;

namespace adventofcode2022;

public class Day21 : Day
{
    public Day21(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var values = new Dictionary<string, long>();
        var operators = new Dictionary<string, (string arg1, string arg2, string op)>();
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"(.*): (.*)");
            var name = match.Groups[1].Value;
            var args = match.Groups[2].Value.Split(" ");
            if (args.Length == 1)
            {
                values[name] = long.Parse(args[0]);
            }
            else
            {
                operators[name] = (args[0], args[2], args[1]);
            }
        }
        while (operators.Count > 0)
        {
            var canEvaluate = operators.Where(op => values.ContainsKey(op.Value.arg1) && values.ContainsKey(op.Value.arg2));
            foreach (var op in canEvaluate)
            {
                var arg1 = values[op.Value.arg1];
                var arg2 = values[op.Value.arg2];
                var result = 0L;
                switch (op.Value.op)
                {
                    case "+":
                        result = arg1 + arg2;
                        break;
                    case "-":
                        result = arg1 - arg2;
                        break;
                    case "/":
                        result = arg1 / arg2;
                        break;
                    case "*":
                        result = arg1 * arg2;
                        break;
                    default:
                        break;
                }
                values[op.Key] = result;
                operators.Remove(op.Key);
            }
        }

        return values["root"].ToString();
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine);
        var values = new Dictionary<string, Func<long>>();
        var operators = new Dictionary<string, (string arg1, string arg2, string op)>();
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"(.*): (.*)");
            var name = match.Groups[1].Value;
            var args = match.Groups[2].Value.Split(" ");
            if (args.Length == 1)
            {
                values[name] = () => long.Parse(args[0]);
            }
            else
            {
                if (name != "root")
                    operators[name] = (args[0], args[2], args[1]);
                else
                    operators[name] = (args[0], args[2], "=");
            }
        }
        var eval = new Dictionary<string, Func<long, long, long>>();
        while (operators.Count > 0)
        {
            var canEvaluate = operators.Where(op => values.ContainsKey(op.Value.arg1) && values.ContainsKey(op.Value.arg2));
            foreach (var op in canEvaluate)
            {
                Func<long> result = null;
                switch (op.Value.op)
                {
                    case "+":
                        result = () => values[op.Value.arg1]() + values[op.Value.arg2]();
                        break;
                    case "-":
                        result = () => values[op.Value.arg1]() - values[op.Value.arg2]();
                        break;
                    case "/":
                        result = () => values[op.Value.arg1]() / values[op.Value.arg2]();
                        break;
                    case "*":
                        result = () => values[op.Value.arg1]() * values[op.Value.arg2]();
                        break;
                    case "=":
                        result = () =>
                        {
                            return values[op.Value.arg1]() - values[op.Value.arg2]();
                        };
                        break;
                    default:
                        break;
                }
                values[op.Key] = result;
                operators.Remove(op.Key);
            }
        }
        var min = 0L;
        var max = 10_000_000_000_000L;

        values["humn"] = () => 0;
        var start = values["root"]();

        values["humn"] = () => max;
        var end = values["root"]();

        var increasing = end > start;

        var current = 0L;
        while (true)
        {
            current = min + ((max - min) / 2);
            values["humn"] = () => current;
            var result = values["root"]();
            if (result == 0)
                break;
            
            if (increasing)
            {
                if (result > 0)
                {
                    max = current;
                }
                else
                {
                    min = current;
                }
            }
            else
            {
                if (result > 0)
                {
                    min = current;
                }
                else
                {
                    max = current;
                }
            }
        }
        while (true)
        {            
            values["humn"] = () => current;
            var result = values["root"]();
            if (result != 0)
                break;
            current--;
        }

        return (current + 1).ToString();
    }
}
