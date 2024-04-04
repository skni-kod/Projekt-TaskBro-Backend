using Application.Contracts.Account;
using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Account.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
{
    private readonly IAccountRepository _accountRepository;

    public RefreshTokenCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _accountRepository.RefreshToken(request.TokenRefresh, cancellationToken);

        if (token is null) throw new UnauthorizedException("Niepoprawny refresh token lub token wygasł");

        return new TokenResponse(token);
    }
}