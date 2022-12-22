namespace TaskTracker.Models;

public class CreateProjectDTO
{
    public string Name { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime CompletitionDate { get; set; }
    public ProjectStatus Status { get; set; }

    public int Priority { get; set; }
}
