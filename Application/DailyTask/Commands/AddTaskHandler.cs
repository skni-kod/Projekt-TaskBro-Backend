using Application.Contracts.Task;
using Application.Persistance.Interfaces.ClaimUserId;
using Application.Persistance.Interfaces.DailyTaskInterface;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace Application.DailyTask.Commands;

public class AddTaskHandler:IRequestHandler<AddTaskCommand,AddTaskResponse>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IGetUserId _getUserId;

    public AddTaskHandler(ITaskRepository taskRepository,IGetUserId getUserId)
    {
        _taskRepository = taskRepository;
        _getUserId = getUserId;
    }
    public async Task<AddTaskResponse> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _getUserId.UserId;
        var newTask = new Domain.Entities.DailyTask(request.Name,request.Description,request.Date,request.Progress,request.Prioryty,userId);
        await _taskRepository.AddTask(userId, newTask, cancellationToken);
        return new AddTaskResponse("Dodano nowego Taska");
    }
}