namespace Application.DailyTask.DTOs;

public class GetDailyTaskDto
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateOnly Date { get; }
    public bool Progress { get;} 
    public int Priority { get;}
    public Guid? UserId { get; set; }
    public GetDailyTaskDto()
    {
        
    }
    public GetDailyTaskDto(string name, string description, DateOnly date, bool progress, int priority)
    {
        Name = name;
        Description = description;
        Date = date;
        Progress = progress;
        Priority = priority;
    }
}