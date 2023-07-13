using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Data.Groups;

internal sealed class IsUserInGroupQuery : IIsUserInGroupQuery
{
    private readonly FroggieDb froggieDb;

    public IsUserInGroupQuery(FroggieDb froggieDb)
    {
        this.froggieDb = froggieDb;
    }

    public async ValueTask<bool> QueryAsync(Id<User> userId, Id<Group> groupId) =>
        await froggieDb.UserGroupMaps.FirstOrDefaultAsync(
            ug => ug.UserId == userId.Value && ug.GroupId == groupId.Value) != null;
}