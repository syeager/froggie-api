using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Data.Users;

internal sealed class UserGroupExistsQuery : IUserGroupExistsQuery
{
    private readonly FroggieDb froggieDb;

    public UserGroupExistsQuery(FroggieDb froggieDb)
    {
        this.froggieDb = froggieDb;
    }

    public async ValueTask<bool> QueryAsync(Id<User> userId, Id<Group> groupId)
    {
        var userGroupExists =
            await froggieDb.GroupUsers.AnyAsync(ugm => ugm.UserId == userId.Value && ugm.GroupId == groupId.Value);

        return userGroupExists;
    }
}