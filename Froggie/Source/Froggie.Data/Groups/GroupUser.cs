using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Domain;

namespace Froggie.Data.Groups;

internal sealed class GroupUser
{
    public Id<Group> GroupId { get; init; }
    public Id<User> UserId { get; init; }
    public Group? Group { get; init; }
    public DomainModel<User>? User { get; init; }
}