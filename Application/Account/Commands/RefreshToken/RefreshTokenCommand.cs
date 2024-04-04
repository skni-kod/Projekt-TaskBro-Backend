using Application.Contracts.Account;
using MediatR;

namespace Application.Account.Commands.RefreshToken;

public record RefreshTokenCommand(
    string TokenRefresh
    ) : IRequest<TokenResponse>;