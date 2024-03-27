using FluentValidation;

namespace Application.Account.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.TokenRefresh)
            .NotEmpty().WithMessage("Token wygasł");
    }
}