using Application.Contracts.Task;
using MediatR;

namespace Application.DailyTask.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid Id,
    string Name,
    string Description,
    DateOnly Date,
    bool Progress,
    int Prioryty):IRequest<UpdateTaskResponse>;