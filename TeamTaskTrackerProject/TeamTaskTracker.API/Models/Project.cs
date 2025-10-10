namespace TeamTaskTracker.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string GoalPeriod { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<TaskProject> Tasks { get; set; } = new List<TaskProject>();
}
