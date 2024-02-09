using LittleByte.Validation;

namespace Froggie.Domain.Groups;

internal sealed class GroupValidator : ModelValidator<Group>
{
    public GroupValidator()
    {
        RuleFor(g => g.Name).IsName();
    }
}