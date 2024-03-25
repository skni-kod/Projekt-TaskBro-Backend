using Application.Account.DTOs;
using Application.Contracts.Account;
using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Account.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(IAccountRepository accountRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _accountRepository = accountRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.SignIn(request.Email, request.Password, cancellationToken);

        if (user is null) throw new BadRequestException("Podaj poprawne dane logowania!");
        
        var token =  _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Password);

        return new TokenResponse(token);
    }
}