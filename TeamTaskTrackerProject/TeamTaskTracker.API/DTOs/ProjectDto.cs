namespace TeamTaskTracker.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string GoalPeriod { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int UserId { get; set; } // Add this field
}
