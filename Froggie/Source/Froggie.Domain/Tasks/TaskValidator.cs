using FluentValidation;
using LittleByte.Validation;

namespace Froggie.Domain.Tasks;

internal sealed class TaskValidator : ModelValidator<Task>
{
    public TaskValidator()
    {
        RuleFor(t => t.CreatorId).IsNotEmpty();
        RuleFor(t => t.Title).IsTitle();
        RuleFor(t => t.DueDate).NotEqual(DateTime.MinValue);
    }
}