using Application.Contracts.Task;
using Application.Persistance.Interfaces.DailyTaskInterface;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace Application.DailyTask.Commands;

public class AddTaskHandler:IRequestHandler<AddTaskCommand,AddTaskResponse>
{
    private readonly ITaskRepository _taskRepository;

    public AddTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<AddTaskResponse> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var id = new Guid();
        var newTask = new Domain.Entities.DailyTask(request.Name,request.Description,request.Date,false,0,id);
        await _taskRepository.AddTask(id, newTask, cancellationToken);
        return new AddTaskResponse("Dodano nowego Taska");
    }
}