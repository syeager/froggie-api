using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common.Infra.Queries;
using LittleByte.Common.Tasks;

namespace Froggie.Domain.Tasks;

public interface IAddUserToTaskService
{
    ValueTask<Task> AddAsync(Id<User> userId, Id<Task> taskId);
}

internal sealed class AddUserToTaskService : IAddUserToTaskService
{
    private readonly IFindByIdQuery<Task> findTaskQuery;
    private readonly IIsUserInGroupQuery isUserInGroupQuery;

    public AddUserToTaskService(IFindByIdQuery<Task> findTaskQuery, IIsUserInGroupQuery isUserInGroupQuery)
    {
        this.findTaskQuery = findTaskQuery;
        this.isUserInGroupQuery = isUserInGroupQuery;
    }

    public async ValueTask<Task> AddAsync(Id<User> userId, Id<Task> taskId)
    {
        var task = await findTaskQuery.FindRequiredForEditAsync(taskId).NoWait();

        var isUserInGroup = await isUserInGroupQuery.QueryAsync(userId, task.GroupId).NoWait();
        if(!isUserInGroup)
        {
            throw new UserNotInGroupException(userId, task.GroupId);
        }

        task.AddAssignee(userId);

        return task;
    }
}