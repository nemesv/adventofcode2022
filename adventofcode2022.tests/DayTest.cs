namespace adventofcode2022.tests;

public class DayTest<TDay>
{
    public TDay Sut(string input)
    {
        return (TDay)System.Activator.CreateInstance(typeof(TDay), input)!;
    }
}