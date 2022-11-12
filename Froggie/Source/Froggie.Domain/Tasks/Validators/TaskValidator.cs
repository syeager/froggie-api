using FluentValidation;

namespace Froggie.Domain.Tasks;

internal class TaskValidator : ModelValidator<Task>
{
    public TaskValidator()
    {
        RuleFor(t => t.CreatorId).IsNotEmpty();
        RuleFor(t => t.Title).IsTitle();
        RuleFor(t => t.DueDate).NotEqual(DateTime.MinValue);
    }
}