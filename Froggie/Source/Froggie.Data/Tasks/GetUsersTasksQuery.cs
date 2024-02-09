using Froggie.Domain.Users;

namespace Froggie.Data.Tasks;

public interface IGetUserTasksQuery
{
    ValueTask<Page<Task>> FindAsync(Id<User> userId);
}

internal sealed class GetUserTasksQuery(IGetTasksByUserQuery tasksQuery) : IGetUserTasksQuery
{
    public async ValueTask<Page<Task>> FindAsync(Id<User> userId)
    {
        var tasks = await tasksQuery.RunAsync(userId);
        return tasks;
    }
}