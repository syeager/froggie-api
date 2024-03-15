using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public interface IAddTaskAssigneeService
{
    ValueTask<Result<Task>> AddAsync(Id<User> userId, Id<Task> taskId);
}

internal sealed class AddTaskAssigneeService(
    IFindByIdQuery<Task> findTaskQuery,
    IFindByIdQuery<User> findUserQuery,
    IIsUserInGroupQuery isUserInGroupQuery)
    : IAddTaskAssigneeService
{
    public async ValueTask<Result<Task>> AddAsync(Id<User> userId, Id<Task> taskId)
    {
        var task = await findTaskQuery.FindRequiredAsync(taskId).NoAwait();
        var user = await findUserQuery.FindRequiredAsync(userId).NoAwait();

        var isUserInGroup = await isUserInGroupQuery.QueryAsync(userId, task.GroupId).NoAwait();
        if(!isUserInGroup)
        {
            return new UserNeedsToBeInTaskGroupToBeAssignee();
        }

        task.AddAssignee(user);

        return Result<Task>.Success(task);
    }
}