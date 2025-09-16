using System.Collections;

namespace Entities;
public class CountdownEnumerator : IEnumerator<int>
{
    private readonly int _start;
    private int _current;
    private bool _started = false;

    public CountdownEnumerator(int start)
    {
        _start = start;
        _current = start + 1; // Start one above so first MoveNext() gives correct value
    }

    // Current element at cursor position
    public int Current { get; private set; }

    // Non-generic version
    object IEnumerator.Current => Current;

    // Move cursor to next position, return true if successful
    public bool MoveNext()
    {
        if (!_started)
        {
            _started = true;
            _current = _start;
        }
        else
        {
            _current--;
        }

        if (_current >= 1)
        {
            Current = _current;
            return true;
        }

        return false; // End of sequence reached
    }

    // Reset cursor to beginning (optional for most scenarios)
    public void Reset()
    {
        _current = _start + 1;
        _started = false;
    }

    // Clean up resources when enumeration is done
    public void Dispose()
    {
        // In this simple example, no cleanup needed
        // But this is where you'd release resources like file handles, database connections, etc.
    }
}