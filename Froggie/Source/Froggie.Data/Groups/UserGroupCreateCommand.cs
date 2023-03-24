using Froggie.Data.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Data.Groups;

internal sealed class UserGroupCreateCommand : IUserGroupCreateCommand
{
    private readonly FroggieDb froggieDb;

    public UserGroupCreateCommand(FroggieDb froggieDb)
    {
        this.froggieDb = froggieDb;
    }

    public void Create(User user, Group group)
    {
        var userGroup = new UserGroupMap
        {
            UserId = user,
            GroupId = group
        };
        froggieDb.UserGroupMaps.Add(userGroup);
    }
}