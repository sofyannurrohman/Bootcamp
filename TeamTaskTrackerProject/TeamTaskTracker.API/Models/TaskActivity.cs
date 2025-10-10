namespace TeamTaskTracker.Models;
public class TaskActivity
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public TaskProject Task { get; set; } = null!;
    public string PreviousStatus { get; set; } = null!;
    public string NewStatus { get; set; } = null!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
