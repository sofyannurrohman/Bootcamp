using System.Collections;

namespace Entities;

public class CountdownSequence : IEnumerable<int>
{
    private readonly int _start;

    public CountdownSequence(int start)
    {
        _start = start;
    }

    // GetEnumerator() returns an enumerator for this sequence
    public IEnumerator<int> GetEnumerator()
    {
        return new CountdownEnumerator(_start);
    }

    // Non-generic version required by IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}