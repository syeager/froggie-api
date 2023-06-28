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

    public AddUserToTaskService(IFindByIdQuery<Task> findTaskQuery)
    {
        this.findTaskQuery = findTaskQuery;
    }

    public async ValueTask<Task> AddAsync(Id<User> userId, Id<Task> taskId)
    {
        var task = await findTaskQuery.FindRequiredForEditAsync(taskId).NoWait();

        task.AddAssignee(userId);

        return task;
    }
}