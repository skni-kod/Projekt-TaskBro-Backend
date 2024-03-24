using FluentValidation;

namespace Application.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().NotEmpty().WithMessage("Podaj adres e-mail!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Imię jest wymagane!");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Nazwisko jest wymagane!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Hasło jest wymagane!")
            .MinimumLength(8).WithMessage("Hasło musi mieć minimum 8 znaków!");

        RuleFor(x => x.RepeatPassword)
            .NotEmpty().WithMessage("Powtórz hasło!")
            .Equal(x => x.Password).WithMessage("Hasła muszą być identyczne!");
    }
}