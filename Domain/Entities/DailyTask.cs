namespace Domain.Entities;

public class DailyTask
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly Date { get; set; }
    public int Progress { get; set; }
    public int Priority { get; set; }

    public Guid? UserId { get; set; }
    public User User { get; set; }
}