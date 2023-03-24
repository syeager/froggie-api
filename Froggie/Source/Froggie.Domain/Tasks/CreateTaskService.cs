using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public interface ICreateTaskService
{
    ValueTask<Task> CreateAsync(string title, Id<User> creatorId, DateTime dueDate, Id<Group> groupId);
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

    public async ValueTask<Task> CreateAsync(string title, Id<User> creatorId, DateTime dueDate, Id<Group> groupId)
    {
        var isUserInGroup = await userGroupExistsQuery.QueryAsync(creatorId, groupId);

        if(!isUserInGroup)
        {
            throw new UserNotInGroupException(creatorId, groupId);
        }

        var id = new Id<Task>();
        var task = taskFactory.Create(id, title, creatorId, dueDate, groupId);
        addTask.Add(task);

        return task;
    }
}