using Application.Contracts.Task;
using MediatR;

namespace Application.DailyTask.Commands.DeleteTask;

public record DeleteTaskCommand(
    Guid Id):IRequest<DeleteTaskResponse>;