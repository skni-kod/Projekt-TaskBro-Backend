using Application.Contracts.Account;
using MediatR;

namespace Application.Account.Commands.CreateAccount;

public record CreateAccountCommand(
    string Name,
    string Surname,
    string Email,
    string Password,
    string RepeatPassword
    ) : IRequest<CreatedAccountResponse>;