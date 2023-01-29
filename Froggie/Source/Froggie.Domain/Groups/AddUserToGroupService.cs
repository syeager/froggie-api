using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public interface IAddUserToGroupService
{
    ValueTask AddAsync(User user, Group group);
}

internal sealed class AddUserToGroupService : IAddUserToGroupService
{
    private readonly IUserGroupExistsQuery existsQuery;
    private readonly IUserGroupCreateCommand createCommand;

    public AddUserToGroupService(IUserGroupExistsQuery existsQuery, IUserGroupCreateCommand createCommand)
    {
        this.existsQuery = existsQuery;
        this.createCommand = createCommand;
    }

    public async ValueTask AddAsync(User user, Group group)
    {
        var alreadyInGroup = await existsQuery.QueryAsync(user, group);
        if(alreadyInGroup)
        {
            throw new Exception();
        }

        createCommand.Create(user, group);
    }
}