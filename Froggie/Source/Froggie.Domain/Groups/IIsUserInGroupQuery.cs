using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public interface IIsUserInGroupQuery
{
    public ValueTask<bool> QueryAsync(Id<User> userId, Id<Group> groupId);
}