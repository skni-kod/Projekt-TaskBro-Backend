using Application.DailyTask.DTOs;
using Application.Persistance.Interfaces.DailyTaskInterface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories.TaskRepositoriers;

public class TaskRepository : ITaskRepository
{
    private readonly TaskBroDbContext _context;

    public TaskRepository(TaskBroDbContext context)
    {
        _context = context;
    }

    public async Task AddTask(Guid userId, DailyTask dailyTask,CancellationToken cancellationToken)
    {
        await _context.Tasks.AddAsync(dailyTask, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
    }

    /* public async Task<List<GetDailyTaskDto>> GetTasks(Guid userId)
     {
         var result = await _context.Tasks.Where(x => x.GetUserId == userId).ToListAsync();
         if (result == null) return null;
         var resulAsDto = result.Select(x => new GetDailyTaskDto(x.Name, x.Description, x.Date, x.Progress, x.Priority))
             .ToList();
         return resulAsDto;
     }
     */

    public async Task<List<GetDailyTaskDto>> GetTasks(Guid UserId)
    {
        var result = await _context.Tasks.Where(x => x.UserId == UserId).ToListAsync();
        if (result == null) return null;
        var resulAsDto = result.Select(x => new GetDailyTaskDto(x.Id,x.Name, x.Description, x.Date, x.Progress, x.Priority,x.UserId)).ToList();
        return resulAsDto;
    }

    public async Task<bool> DeleteTask(Guid UserId, Guid TaskId,CancellationToken cancellationToken)
    {
        var result = await _context.Tasks.FindAsync(TaskId,cancellationToken);
        if (result.UserId != UserId)
        {
            return false;
        }

        _context.Tasks.Remove(result);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}