using FluentValidation;

namespace Application.DailyTask.Commands.UpdateTask;

public class UpdateTaskValidator:AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Podaj Id taska");
    }
}