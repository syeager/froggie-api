namespace Froggie.Domain.Groups;

public interface IGroupFactory
{
    Group Create(Guid id, string nameValue);
}

internal sealed class GroupFactory : IGroupFactory
{
    private readonly IModelValidator<Group> validator;

    public GroupFactory(IModelValidator<Group> validator)
    {
        this.validator = validator;
    }

    public Group Create(Guid id, string nameValue)
    {
        var name = new Name(nameValue);
        var group = Group.Create(validator, id, name);
        return group;
    }
}