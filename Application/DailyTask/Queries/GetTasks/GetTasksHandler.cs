using Application.Contracts.Task;
using Application.Persistance.Interfaces.DailyTaskInterface;
using MediatR;

namespace Application.DailyTask.Queries.GetTasks;

public class GetTasksHandler:IRequestHandler<GetTasksQuery,GetTasksResponse>
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<GetTasksResponse> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var result = await _taskRepository.GetTasks();
                                //where id
        return new GetTasksResponse(result);
    }
}