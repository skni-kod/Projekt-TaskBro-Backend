using Application.DailyTask.DTOs;
using Domain.Entities;

namespace Application.Persistance.Interfaces.DailyTaskInterface;

public interface ITaskRepository
{
    Task AddTask(Guid userId, Domain.Entities.DailyTask dailyTask,CancellationToken cancellationToken);
    // Task<List<GetDailyTaskDto>> GetTasks(Guid userId);
    Task<List<GetDailyTaskDto>> GetTasks();
}