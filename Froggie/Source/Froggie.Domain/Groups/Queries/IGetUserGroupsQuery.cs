using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public interface IGetUserGroupsQuery
{
    ValueTask<IReadOnlyCollection<Group>> QueryAsync(Id<User> userId);
}