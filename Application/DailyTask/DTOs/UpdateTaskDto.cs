namespace Application.DailyTask.DTOs;

public class UpdateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly Date { get; set; }
    public bool Progress { get; set; } 
    public int Priority { get; set; }
}