using System.Reflection;

var dayNumber = DateTime.Now.AddDays(-1).Day;
if (args.Length > 0)
{
    dayNumber = int.Parse(args[0]);
}
var currentDate = dayNumber.ToString().PadLeft(2, '0'); ;
var dayType = Assembly.GetExecutingAssembly().GetType($"adventofcode2022.Day{currentDate}")!;
var currentDay = (Day)Activator.CreateInstance(dayType, File.ReadAllText($"Inputs\\Day{currentDate}.txt"))!;
Console.WriteLine(currentDay.Part1());
Console.WriteLine(currentDay.Part2());

Console.ReadKey();