namespace Froggie.Domain.Groups;

public sealed class Group : DomainModel<Group>
{
    public Name Name { get; }

    private Group(Id<Group> id, Name name)
        : base(id)
    {
        Name = name;
    }

    internal static Group Create(IModelValidator<Group> validator, Id<Group> id, Name name)
    {
        var group = new Group(id, name);
        validator.SignOrThrow(group);
        return group;
    }
}