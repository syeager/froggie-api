using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public interface IAddUserToTaskService
{
    ValueTask<Task> AddAsync(Id<User> userId, Id<Task> taskId);
}

internal sealed class AddUserToTaskService(IFindByIdQuery<Task> findTaskQuery, IIsUserInGroupQuery isUserInGroupQuery)
    : IAddUserToTaskService
{
    public async ValueTask<Task> AddAsync(Id<User> userId, Id<Task> taskId)
    {
        var task = await findTaskQuery.FindRequiredForEditAsync(taskId).NoAwait();

        var isUserInGroup = await isUserInGroupQuery.QueryAsync(userId, task.GroupId).NoAwait();
        if(!isUserInGroup)
        {
            throw new UserNotInGroupException(userId, task.GroupId);
        }

        task.AddAssignee(userId);

        return task;
    }
}