using Application.DailyTask.Commands;
using Application.DailyTask.Queries.GetTasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Task;

[Route("task")]
[Authorize]
public class TaskController: BaseController
{
    [HttpPost("/SetTask")]
    public async Task<IActionResult> SetTask([FromBody] AddTaskCommand command,CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet("/GetTasks")]
    public async Task<IActionResult> GetTasks( CancellationToken cancellationToken)

    {
        var query = new GetTasksQuery();
        var response = await Mediator.Send(query, cancellationToken);

        return Ok(response);
    }
}