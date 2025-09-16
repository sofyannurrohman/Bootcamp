using BootcampDay6.Entities;
namespace BootcampDay6.OperatorOverloading;
public class OperatorOverloadingClassID {
public static void IncrementDecrementDemo()
        {
            Console.WriteLine("9. INCREMENT AND DECREMENT OPERATORS");
            Console.WriteLine("====================================");

            Counter counter = new Counter(5);
            Console.WriteLine($"Initial counter: {counter.Value}");

            // Pre-increment
            Counter preInc = ++counter;
            Console.WriteLine($"After pre-increment (++counter): counter={counter.Value}, returned={preInc.Value}");

            // Post-increment
            Counter postInc = counter++;
            Console.WriteLine($"After post-increment (counter++): counter={counter.Value}, returned={postInc.Value}");

            // Pre-decrement
            Counter preDec = --counter;
            Console.WriteLine($"After pre-decrement (--counter): counter={counter.Value}, returned={preDec.Value}");

            // Post-decrement
            Counter postDec = counter--;
            Console.WriteLine($"After post-decrement (counter--): counter={counter.Value}, returned={postDec.Value}");

            Console.WriteLine();
        }
}
