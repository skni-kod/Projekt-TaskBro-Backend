using Application.Contracts.Task;
using Application.DailyTask.DTOs;
using Application.Persistance.Interfaces.ClaimUserId;
using Application.Persistance.Interfaces.DailyTaskInterface;
using MediatR;

namespace Application.DailyTask.Commands.UpdateTask;

public class UpdateTaskHandler:IRequestHandler<UpdateTaskCommand,UpdateTaskResponse>
{

    private readonly ITaskRepository _taskRepository;
    private readonly IGetUserId _getUserId;

    public UpdateTaskHandler(ITaskRepository taskRepository,IGetUserId getUserId)
    {
        _taskRepository = taskRepository;
        _getUserId = getUserId;
    }
    public async Task<UpdateTaskResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _getUserId.UserId;
        var updateTask = new UpdateTaskDto
        {
            Name = request.Name,
            Description = request.Description,
            Date = request.Date,
            Progress = request.Progress,
            Priority = request.Prioryty
        };
        var result=await _taskRepository.UpdateTask(userId, request.Id, updateTask, cancellationToken);
        if (result == false)
        {
            return new UpdateTaskResponse("Nie zaktualizowano taska");
        }
        return new UpdateTaskResponse("Task został zaktualizowany");
    }
}