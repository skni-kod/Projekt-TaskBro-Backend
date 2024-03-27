using Application.Contracts.Account;
using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Account.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
{
    private readonly IAccountRepository _accountRepository;

    public LoginCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var token = await _accountRepository.SignIn(request.Email, request.Password, cancellationToken);

        if (token is null) throw new BadRequestException("Podaj poprawne dane logowania!");

        return new TokenResponse(token);
    }
}