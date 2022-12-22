
public enum TaskStatus {
    ToDo,
    InProgress,
    Done
}

public class ProjectTask
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TaskStatus Status {get; set;}

    public string Description {get; set;}
    public int Priority { get; set;}
    public Project Project { get; set; }

}