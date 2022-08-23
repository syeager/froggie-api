using LittleByte.Validation;

namespace Froggie.Domain.Tasks.Validators;

internal class TaskValidator : ModelValidator<Task>
{
    public TaskValidator()
    {
        RuleFor(t => t.Title).IsTitle();
    }
}