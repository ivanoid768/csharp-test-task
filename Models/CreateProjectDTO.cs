using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models;

public class CreateProjectDTO
{
    [Required]
    public string Name { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime CompletitionDate { get; set; }

    [Required]
    [Range(0, 2)]
    public ProjectStatus Status { get; set; }

    [Required]
    public int Priority { get; set; }
}
