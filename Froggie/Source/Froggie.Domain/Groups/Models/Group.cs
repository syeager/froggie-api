namespace Froggie.Domain.Groups;

public sealed class Group : DomainModel<Group>
{
    private Group(Id<Group> id)
        : base(id) { }
}