using FluentValidation;

namespace Application.Account.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail nie może być pusty!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Hasło nie może być puste!");
    }
}