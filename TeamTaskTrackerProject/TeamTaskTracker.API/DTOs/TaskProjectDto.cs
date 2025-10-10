namespace TeamTaskTracker.DTOs;
public class TaskProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Status { get; set; } = null!;
    public int AssignedToUserId { get; set; }
    public int ProjectId { get; set; }
}
