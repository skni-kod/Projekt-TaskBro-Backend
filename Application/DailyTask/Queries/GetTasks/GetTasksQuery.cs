using Application.Contracts.Task;
using MediatR;

namespace Application.DailyTask.Queries.GetTasks;

public record GetTasksQuery() : IRequest<GetTasksResponse>;