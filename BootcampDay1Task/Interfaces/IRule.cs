namespace BootcampDay1Task.Interfaces
{
    public interface IRule
    {
        int Divisor { get; }
        string Word { get; }
        bool AppliesTo(int x);
    }
}
