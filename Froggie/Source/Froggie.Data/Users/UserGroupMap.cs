using Froggie.Data.Groups;

namespace Froggie.Data.Users;

internal sealed class UserGroupMap
{
    public Guid UserId { get; init; }
    public Guid GroupId { get; init; }
    public UserDao? User { get; init; }
    public GroupDao? Group { get; init; }
}