using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Data.Groups;

internal sealed class IsUserInGroupQuery(FroggieDb froggieDb) : IIsUserInGroupQuery
{
    public async ValueTask<bool> QueryAsync(Id<User> userId, Id<Group> groupId) =>
        await froggieDb.GroupUsers.FirstOrDefaultAsync(
            ug => ug.UserId == userId.Value && ug.GroupId == groupId.Value) != null;
}