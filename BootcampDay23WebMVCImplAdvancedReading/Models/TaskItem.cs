namespace BootcampDay23AdvancedReading.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public TaskItem(string title)
    {
        Title = title;
        IsCompleted = false;
    }
    // Note : Applied Tell dont Ask
    public void Completed()
    {
        if (IsCompleted)
        {
            throw new InvalidOperationException("Task already completed.");
        }
        IsCompleted = true;
    }
}