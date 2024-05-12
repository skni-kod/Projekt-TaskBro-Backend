using Application.DailyTask.Commands;
using Application.DailyTask.Commands.DeleteTask;
using Application.DailyTask.Commands.UpdateTask;
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

    [HttpDelete("/DeleteTask")]
    public async Task<IActionResult> DeleteTask([FromBody] DeleteTaskCommand command,CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPut("/UpdateTask")]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);
        return Ok(response);
    }
}