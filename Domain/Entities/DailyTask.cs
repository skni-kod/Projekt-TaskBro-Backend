namespace Domain.Entities;

public class DailyTask
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly Date { get; set; }
    public bool Progress { get; set; }
    public int Priority { get; set; }

    public Guid? UserId { get; set; }
    public User User { get; set; }

    public DailyTask()
    {
        
    }
    public DailyTask(string name, string description, DateOnly date, bool progress, int priority,Guid userid)
    {
        UserId = userid;
        Name = name;
        Description = description;
        Date = date;
        Progress = progress;
        Priority = priority;
    }
    
}