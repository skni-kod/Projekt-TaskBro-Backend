using Application.Contracts.Account;
using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreatedAccountResponse>
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<CreatedAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var isEmailExist = await _accountRepository.IsEmailExist(request.Email, cancellationToken);

        if (isEmailExist) throw new Conflict("E-mail jest zajęty!");

        var newUser = new User(request.Name, request.Surname, request.Email, request.Password);

        var newUserId = await _accountRepository.CreateAccount(newUser, cancellationToken);

        return new CreatedAccountResponse(newUserId);
    }
}