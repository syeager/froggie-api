using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public sealed record UserAddedToTask(Task Task) : OperationResult(true);

public sealed record UserNotInTaskGroup() : OperationResult(false, "User needs to be in the same group as the task.");


public interface IAddUserToTaskService
{
    ValueTask<OperationResult> AddAsync(Id<User> userId, Id<Task> taskId);
}

internal sealed class AddUserToTaskService(IFindByIdQuery<Task> findTaskQuery, IIsUserInGroupQuery isUserInGroupQuery)
    : IAddUserToTaskService
{
    public async ValueTask<OperationResult> AddAsync(Id<User> userId, Id<Task> taskId)
    {
        var task = await findTaskQuery.FindRequiredForEditAsync(taskId).NoAwait();

        var isUserInGroup = await isUserInGroupQuery.QueryAsync(userId, task.GroupId).NoAwait();
        if(!isUserInGroup)
        {
            return new UserNotInTaskGroup();
        }

        task.AddAssignee(userId);

        return new UserAddedToTask(task);
    }
}