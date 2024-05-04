using FluentValidation;

namespace Application.DailyTask.Commands;

public class AddTaskValidator :AbstractValidator<AddTaskCommand>
{
    public AddTaskValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Podaj nazwę Taska");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Podaj opis Taska");
    }
}