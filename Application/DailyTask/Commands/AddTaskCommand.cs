using Application.Contracts.Task;
using MediatR;

namespace Application.DailyTask.Commands;

public record AddTaskCommand(
    string Name,
    string? Description,
    DateOnly Date,
    bool? Progress,
    int? Prioryty) : IRequest<AddTaskResponse>;