using LittleByte.Validation;

namespace Froggie.Domain.Groups;

public interface IGroupFactory
{
    Group Create(Id<Group> id, string nameValue);
}

internal sealed class GroupFactory : IGroupFactory
{
    private readonly IModelValidator<Group> validator;

    public GroupFactory(IModelValidator<Group> validator)
    {
        this.validator = validator;
    }

    public Group Create(Id<Group> id, string nameValue)
    {
        var name = new Name(nameValue);
        var group = Group.Create(validator, id, name);
        return group;
    }
}