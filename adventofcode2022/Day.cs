public abstract class Day
{
    protected string input;

    public Day(string input)
    {
        this.input = input;
    }

    public abstract string Part1();

    public abstract string Part2();
}