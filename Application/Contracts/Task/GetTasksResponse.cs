using Application.DailyTask.DTOs;

namespace Application.Contracts.Task;

public record GetTasksResponse(List<GetDailyTaskDto> Data);