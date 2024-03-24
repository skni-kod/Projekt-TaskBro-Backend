using Application.Account.Commands.CreateAccount;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Areas.Auth;

[Route("api")]
public class AccountController : BaseController
{
    /// <summary>
    /// Create new account
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("/register")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    //s
}