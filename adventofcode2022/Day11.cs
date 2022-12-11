using System.Numerics;
using System.Security.AccessControl;

namespace adventofcode2022;

public class Day11 : Day
{
    public Day11(string input) : base(input)
    {
    }

    public override string Part1()
    {
        var monkeys = new List<Monkey>() {
            new Monkey()
            {
                Items = { 85, 79, 63, 72 },
                Operation = (old) => old * 17,
                Test = 2,
                TrueTarget = 2,
                FalseTarget = 6
            },
            new Monkey()
            {
                Items = { 53, 94, 65, 81, 93, 73, 57, 92 },
                Operation = (old) => old * old,
                Test = 7,
                TrueTarget = 0,
                FalseTarget = 2
            },
            new Monkey()
            {
                Items = { 62, 63 },
                Operation = (old) => old + 7,
                Test = 13,
                TrueTarget = 7,
                FalseTarget = 6
            },
            new Monkey()
            {
                Items = { 57, 92, 56 },
                Operation = (old) => old + 4,
                Test = 5,
                TrueTarget = 4,
                FalseTarget = 5
            },
            new Monkey()
            {
                Items = { 67 },
                Operation = (old) => old + 5,
                Test = 3,
                TrueTarget = 1,
                FalseTarget = 5
            },
            new Monkey()
            {
                Items = { 85, 56, 66, 72, 57, 99 },
                Operation = (old) => old + 6,
                Test = 19,
                TrueTarget = 1,
                FalseTarget = 0
            },
            new Monkey()
            {
                Items = { 86, 65, 98, 97, 69 },
                Operation = (old) =>  old * 13,
                Test = 11,
                TrueTarget = 3,
                FalseTarget = 7
            },
            new Monkey()
            {
                Items = { 87, 68, 92, 66, 91, 50, 68 },
                Operation = (old) => old + 2,
                Test = 17,
                TrueTarget = 4,
                FalseTarget = 3
            }
        };
        var rounds = 20;
        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    monkey.NumberOfInspections += 1;
                    var next = monkey.Operation(item);
                    next = next / 3;
                    if (next % monkey.Test == 0)
                    {
                        monkeys[monkey.TrueTarget].Items.Add(next);
                    }
                    else
                    {
                        monkeys[monkey.FalseTarget].Items.Add(next);
                    }
                }
                monkey.Items.Clear();
            }
        }

        var result = monkeys.OrderByDescending(m => m.NumberOfInspections).ToList();
        return (result[0].NumberOfInspections * result[1].NumberOfInspections).ToString();
    }

    public override string Part2()
    {
        var monkeys = new List<Monkey>() {
            new Monkey()
            {
                Items = { 85, 79, 63, 72 },
                Operation = (old) => old * 17,
                Test = 2,
                TrueTarget = 2,
                FalseTarget = 6
            },
            new Monkey()
            {
                Items = { 53, 94, 65, 81, 93, 73, 57, 92 },
                Operation = (old) => old * old,
                Test = 7,
                TrueTarget = 0,
                FalseTarget = 2
            },
            new Monkey()
            {
                Items = { 62, 63 },
                Operation = (old) => old + 7,
                Test = 13,
                TrueTarget = 7,
                FalseTarget = 6
            },
            new Monkey()
            {
                Items = { 57, 92, 56 },
                Operation = (old) => old + 4,
                Test = 5,
                TrueTarget = 4,
                FalseTarget = 5
            },
            new Monkey()
            {
                Items = { 67 },
                Operation = (old) => old + 5,
                Test = 3,
                TrueTarget = 1,
                FalseTarget = 5
            },
            new Monkey()
            {
                Items = { 85, 56, 66, 72, 57, 99 },
                Operation = (old) => old + 6,
                Test = 19,
                TrueTarget = 1,
                FalseTarget = 0
            },
            new Monkey()
            {
                Items = { 86, 65, 98, 97, 69 },
                Operation = (old) =>  old * 13,
                Test = 11,
                TrueTarget = 3,
                FalseTarget = 7
            },
            new Monkey()
            {
                Items = { 87, 68, 92, 66, 91, 50, 68 },
                Operation = (old) => old + 2,
                Test = 17,
                TrueTarget = 4,
                FalseTarget = 3
            }
        };
        var rounds = 10000;
        var common = monkeys.Aggregate(1, (acc, c) => acc * c.Test);
        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    monkey.NumberOfInspections += 1;
                    var next = monkey.Operation(item);

                    next = next % common;

                    if (next % monkey.Test == 0)
                    {                        
                        monkeys[monkey.TrueTarget].Items.Add(next);
                    }
                    else
                    {
                        monkeys[monkey.FalseTarget].Items.Add(next);
                    }
                }
                monkey.Items.Clear();
            }
        }
        var result = monkeys.OrderByDescending(m => m.NumberOfInspections).ToList();
        return (result[0].NumberOfInspections * result[1].NumberOfInspections).ToString();
    }
}



public class Monkey
{
    public List<BigInteger> Items { get; set; } = new List<BigInteger>();

    public Func<BigInteger, BigInteger> Operation { get; set; }

    public int Test { get; set; }

    public int TrueTarget { get; set; }

    public int FalseTarget { get; set; }

    public long NumberOfInspections { get; set; }
}

