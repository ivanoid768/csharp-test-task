using System.ComponentModel.DataAnnotations;

public class CreateProjectTaskDTO
{
    [Required]
    public string Name { get; set; }

    [Range(0, 2)]
    public TaskStatus Status {get; set;}

    [Required]
    public string Description {get; set;}

    [Required]
    public int Priority { get; set;}
}