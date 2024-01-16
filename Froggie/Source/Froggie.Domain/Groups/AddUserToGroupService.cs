using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public sealed record UserAlreadyInGroup() : OperationResult(false, "User is already in this group.");

public interface IAddUserToGroupService
{
    ValueTask<OperationResult> AddAsync(User user, Group group);
}

internal sealed class AddUserToGroupService(IUserGroupExistsQuery existsQuery, IUserGroupCreateCommand createCommand)
    : IAddUserToGroupService
{
    public async ValueTask<OperationResult> AddAsync(User user, Group group)
    {
        var alreadyInGroup = await existsQuery.QueryAsync(user, group);
        if(alreadyInGroup)
        {
            return new UserAlreadyInGroup();
        }

        createCommand.Create(user, group);

        return new OperationResult(true);
    }
}