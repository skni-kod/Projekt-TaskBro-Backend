namespace Application.DailyTask.DTOs;

public class GetDailyTaskDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateOnly Date { get; set; }
    public bool? Progress { get; set; } 
    public int? Priority { get; set; }
    
    public Guid? UserId { get; set; }
    public GetDailyTaskDto()
    {
        
    }
    public GetDailyTaskDto(Guid id,string name, string? description, DateOnly date, bool? progress, int? priority,Guid userid)
    {
        Id = id;
        Name = name;
        Description = description;
        Date = date;
        Progress = progress;
        Priority = priority;
        UserId = userid;
    }
}