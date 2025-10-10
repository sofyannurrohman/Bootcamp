namespace TeamTaskTracker.DTOs;
public class TaskActivityDto
{
    public int Id { get; set; }
    public int TaskProjectId { get; set; }
    public string PreviousStatus { get; set; } = null!;
    public string NewStatus { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}
