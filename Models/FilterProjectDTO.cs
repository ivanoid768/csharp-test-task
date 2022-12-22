public enum SortBy{
    Id,
    Name,
    StartDate,
    CompletitionDate,
    Status,
    Priority
}

public class FilterProjectDTO
{
    public List<int> Ids {get; set;}
    public string SearchName { get; set; }

    public DateTime StartDateFrom { get; set; }
    public DateTime StartDateTo { get; set; }
    public DateTime CompletitionDateFrom { get; set; }
    public DateTime CompletitionDateTo { get; set; }
    public List<ProjectStatus> Status { get; set; }

    public List<int> Priority { get; set; }

    public int Page {get; set;}
    public int PerPage {get; set;}

    public SortBy sort {get; set;}
}