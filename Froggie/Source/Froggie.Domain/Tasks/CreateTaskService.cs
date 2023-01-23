using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public interface ICreateTaskService
{
    ValueTask<Task> CreateAsync(string title, Guid creatorId, DateTime dueDate, Guid groupId);
}

internal sealed class CreateTaskService : ICreateTaskService
{
    private readonly IAddTaskCommand addTask;
    private readonly IUserGroupExistsQuery userGroupExistsQuery;
    private readonly ITaskFactory taskFactory;

    public CreateTaskService(IAddTaskCommand addTask, IUserGroupExistsQuery userGroupExistsQuery, ITaskFactory taskFactory)
    {
        this.addTask = addTask;
        this.userGroupExistsQuery = userGroupExistsQuery;
        this.taskFactory = taskFactory;
    }

    public async ValueTask<Task> CreateAsync(string title, Guid creatorId, DateTime dueDate, Guid groupId)
    {
        // TODO: Validate user is in group.
        var isUserInGroup = await userGroupExistsQuery.QueryAsync(creatorId, groupId);

        if(!isUserInGroup)
        {
            throw new Exception();
        }

        var id = Guid.NewGuid();
        var task = taskFactory.Create(id, title, creatorId, dueDate, groupId);
        addTask.Add(task);

        return task;
    }
}