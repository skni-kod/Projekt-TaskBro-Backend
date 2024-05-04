using Application.Contracts.Task;
using Application.Persistance.Interfaces.ClaimUserId;
using Application.Persistance.Interfaces.DailyTaskInterface;
using MediatR;

namespace Application.DailyTask.Queries.GetTasks;

public class GetTasksHandler:IRequestHandler<GetTasksQuery,GetTasksResponse>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IGetUserId _getUserId;

    public GetTasksHandler(ITaskRepository taskRepository,IGetUserId getUserId)
    {
        _taskRepository = taskRepository;
        _getUserId = getUserId;
    }
    public async Task<GetTasksResponse> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var UserId = _getUserId.UserId;
        var result = await _taskRepository.GetTasks(UserId);
        return new GetTasksResponse(result);
    }
}