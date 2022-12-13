using System.ComponentModel;
using System.Security.AccessControl;

namespace adventofcode2022;

public class Day13 : Day
{
    public Day13(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var lines = input.Split(Environment.NewLine);
        var result = 0;
        var index = 1;
        for (int i = 0; i < lines.Length; i += 3)
        {
            var leftString = lines[i];
            var rightString = lines[i + 1];
            var left = Parse(leftString);
            var right = Parse(rightString);

            var isInOrder = IsInOrder(left, right);

            if (isInOrder == true)
            {
                result += index;
            }

            index++;
        }

        return result.ToString();
    }

    public bool? IsInOrder(Signal left, Signal right)
    {
        if (left.Value.HasValue && right.Value.HasValue)
        {
            if (left.Value == right.Value)
            {
                return null;
            }
            return left.Value < right.Value;
        }
        else if (left.Value == null && right.Value == null)
        {
            for (int i = 0; i < left.Inner.Count; i++)
            {
                if (i < right.Inner.Count)
                {
                    var result = IsInOrder(left.Inner[i], right.Inner[i]);
                    if (result.HasValue)
                        return result.Value;
                }
                else
                {
                    return false;
                }
            }
            return left.Inner.Count < right.Inner.Count ? true : null;
        }
        else if (left.Value == null)
        {
            var wrapper = new Signal();
            wrapper.Inner.Add(new Signal() { Value = right.Value });
            return IsInOrder(left, wrapper);
        }
        else //right.Value = null
        {
            var wrapper = new Signal();
            wrapper.Inner.Add(new Signal() { Value = left.Value });
            return IsInOrder(wrapper, right);
        }
    }

    public Signal Parse(string inputString)
    {
        Signal root = null;
        Signal current = null;
        for (int j = 0; j < inputString.Length - 1; j++)
        {
            switch (inputString[j])
            {
                case '[':
                    var newSignal = new Signal();
                    if (root == null)
                    {
                        root = newSignal;
                    }
                    else
                    {
                        newSignal.Parent = current;
                        current.Inner.Add(newSignal);
                    }
                    current = newSignal;
                    break;
                case ']':
                    current = current.Parent;
                    break;
                case ',':
                    break;
                default:
                    var n = inputString[j].ToString();
                    if (char.IsDigit(inputString[j + 1]))
                        n += inputString[j + 1];
                    current.Inner.Add(new Signal() { Value = int.Parse(n), Parent = current });
                    break;
            }
        }
        return root;
    }

    public override string Part2()
    {
        var lines = input.Split(Environment.NewLine);
        var result = 0;
        var index = 1;
        var signals = new List<Signal>();
        for (int i = 0; i < lines.Length; i++)
        {
            if (!string.IsNullOrEmpty(lines[i]))
                signals.Add(Parse(lines[i]));
        }
        var two = new Signal()
        {
            Inner = { new Signal() { Inner = { new Signal() { Value = 2 } } } }
        };
        var six = new Signal()
        {
            Inner = { new Signal() { Inner = { new Signal() { Value = 6 } } } }
        };
        signals.Add(two);
        signals.Add(six);

        signals.Sort((x, y) =>
        {
            switch (IsInOrder(y, x))
            {
                case true:
                    return 1;
                case false:
                    return -1;
                case null:
                    return 0;
            }
        });

        return ((signals.IndexOf(two) + 1) * (signals.IndexOf(six) + 1)).ToString();
    }

    public class Signal
    {
        public int? Value { get; set; }

        public List<Signal> Inner { get; } = new List<Signal>();

        public Signal Parent { get; set; }
    }
}
