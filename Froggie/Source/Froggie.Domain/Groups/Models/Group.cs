namespace Froggie.Domain.Groups.Models;

public sealed class Group : DomainModel<Group>
{
    private Group(Id<Group> id)
        : base(id) { }
}