namespace BootcampDay5.EventHandler;

public class EventHandlerClassTSEH
{
    static void ThreadSafeEventHandlingDemo()
    {
        Console.WriteLine("6. THREAD-SAFE EVENT HANDLING");
        Console.WriteLine("=============================");

        var processor = new DataProcessor();
        var logger = new EventLogger();

        // Subscribe to events
        processor.DataProcessed += logger.LogDataProcessed;
        processor.DataProcessed += (sender, e) =>
            Console.WriteLine($"  Processing complete: {e.ItemsProcessed} items in {e.Duration.TotalMilliseconds}ms");

        Console.WriteLine("Starting concurrent data processing...");

        // Simulate multiple threads processing data
        var threads = new Thread[3];
        for (int i = 0; i < threads.Length; i++)
        {
            int threadId = i;
            threads[i] = new Thread(() =>
            {
                processor.ProcessData($"Dataset-{threadId}", 100 + threadId * 50);
            });
            threads[i].Start();
        }

        // Wait for all threads to complete
        foreach (var thread in threads)
            thread.Join();

        Console.WriteLine("All processing completed safely\n");
    }
}

public class DataProcessedEventArgs : EventArgs
{
    public string DatasetName { get; }
    public int ItemsProcessed { get; }
    public TimeSpan Duration { get; }
    public int ThreadId { get; }

    public DataProcessedEventArgs(string datasetName, int itemsProcessed, TimeSpan duration)
    {
        DatasetName = datasetName;
        ItemsProcessed = itemsProcessed;
        Duration = duration;
        ThreadId = Thread.CurrentThread.ManagedThreadId;
    }
}
public class DataProcessor
{
    // Thread-safe event declaration
    public event EventHandler<DataProcessedEventArgs>? DataProcessed;

    public void ProcessData(string datasetName, int itemCount)
    {
        var startTime = DateTime.Now;

        Console.WriteLine($"  Thread {Thread.CurrentThread.ManagedThreadId}: Processing {datasetName}...");

        // Simulate processing time
        Thread.Sleep(100 + new Random().Next(100));

        var duration = DateTime.Now - startTime;

        // Fire the event - this is thread-safe due to the null-conditional operator
        OnDataProcessed(new DataProcessedEventArgs(datasetName, itemCount, duration));
    }


    protected virtual void OnDataProcessed(DataProcessedEventArgs e)
    {
        // The ?. operator ensures thread safety even if subscribers change
        // between threads
        DataProcessed?.Invoke(this, e);
    }

}
public class EventLogger
{
    public void LogDataProcessed(object? sender, DataProcessedEventArgs e)
    {
        Console.WriteLine($"  Logger: {e.DatasetName} completed on thread {e.ThreadId} " +
                        $"({e.ItemsProcessed} items, {e.Duration.TotalMilliseconds:F0}ms)");
    }
}