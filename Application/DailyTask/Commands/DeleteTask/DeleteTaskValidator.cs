using FluentValidation;

namespace Application.DailyTask.Commands.DeleteTask;

public class DeleteTaskValidator:AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Podaj Id Taska");
    }
}