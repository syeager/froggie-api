namespace Froggie.Domain.Tasks;

internal class TaskValidator : ModelValidator<Task>
{
    public TaskValidator()
    {
        RuleFor(t => t.Title).IsTitle();
    }
}