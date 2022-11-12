namespace Froggie.Domain.Groups;

public sealed class Group : DomainModel<Group>
{
    public Name Name { get; }

    private Group(Id<Group> id, Name name)
        : base(id)
    {
        Name = name;
}
}