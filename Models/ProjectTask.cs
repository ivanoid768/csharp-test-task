
using System.ComponentModel.DataAnnotations;

public enum TaskStatus {
    ToDo,
    InProgress,
    Done
}

public class ProjectTask
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    [Range(0, 2)]
    public TaskStatus Status {get; set;}

    public string Description {get; set;}

    [Range(0, 5)]
    public int Priority { get; set;}
    public Project Project { get; set; }

}