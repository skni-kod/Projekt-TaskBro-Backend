using Application.Contracts.Account;
using MediatR;

namespace Application.Account.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
    ) : IRequest<TokenResponse>;