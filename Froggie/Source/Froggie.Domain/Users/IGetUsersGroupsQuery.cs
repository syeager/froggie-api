using Froggie.Domain.Groups;

namespace Froggie.Domain.Users;

public interface IGetUsersGroupsQuery
{
    ValueTask<IReadOnlyCollection<Group>> QueryAsync(Id<User> userId);
}