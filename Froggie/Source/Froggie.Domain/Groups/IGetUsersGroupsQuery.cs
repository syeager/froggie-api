using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public interface IGetUsersGroupsQuery
{
    ValueTask<IReadOnlyCollection<Group>> QueryAsync(Id<User> userId);
}