namespace TeamTaskTracker.Models;
public class TaskProject
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Type { get; set; } = "UserStory"; // UserStory / Issue
    public string Status { get; set; } = "Pending";
    public int AssignedToUserId { get; set; }
    public User AssignedTo { get; set; } = null!;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public ICollection<TaskActivity> Activities { get; set; } = new List<TaskActivity>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
