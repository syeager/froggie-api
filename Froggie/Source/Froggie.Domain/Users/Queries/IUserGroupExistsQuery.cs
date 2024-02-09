using Froggie.Domain.Groups;

namespace Froggie.Domain.Users;

public interface IUserGroupExistsQuery
{
    public ValueTask<bool> QueryAsync(Id<User> userId, Id<Group> groupId);
}