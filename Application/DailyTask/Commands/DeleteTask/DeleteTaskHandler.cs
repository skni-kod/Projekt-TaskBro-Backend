using Application.Contracts.Task;
using Application.Persistance.Interfaces.ClaimUserId;
using Application.Persistance.Interfaces.DailyTaskInterface;
using MediatR;

namespace Application.DailyTask.Commands.DeleteTask;

public class DeleteTaskHandler:IRequestHandler<DeleteTaskCommand,DeleteTaskResponse>
{
    private readonly IGetUserId _getUserId;
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskHandler(IGetUserId getUserId,ITaskRepository taskRepository)
    {
        _getUserId = getUserId;
        _taskRepository = taskRepository;
    }
    public async Task<DeleteTaskResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _getUserId.UserId;
        var response = await _taskRepository.DeleteTask(userId, request.Id, cancellationToken);
        if (response == true)
        {
            return new DeleteTaskResponse("Usunięto Taska");
        }
        else
        {
            return new DeleteTaskResponse("Nie ma takiego taska");
        }
    }
}