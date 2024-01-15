using Froggie.Domain.Users;

namespace Froggie.Data.Tasks;

public interface IGetUsersTasksService
{
    ValueTask<Page<Task>> FindAsync(Id<User> userId);
}

internal sealed class GetUsersTasksService(IGetTasksByUserQuery tasksQuery) : IGetUsersTasksService
{
    public async ValueTask<Page<Task>> FindAsync(Id<User> userId)
    {
        var tasks = await tasksQuery.RunAsync(userId);
        return tasks;
    }
}