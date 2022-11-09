using Froggie.Domain.Users;
using LittleByte.Common.Infra.Models;

namespace Froggie.Domain.Tasks;

public interface IGetUsersTasksService
{
    ValueTask<PageResponse<Task>> FindAsync(Id<User> userId);
}

internal sealed class GetUsersTasksService : IGetUsersTasksService
{
    private readonly IGetTasksByUserQuery getTasksQuery;

    public GetUsersTasksService(IGetTasksByUserQuery getTasksQuery)
    {
        this.getTasksQuery = getTasksQuery;
    }

    public async ValueTask<PageResponse<Task>> FindAsync(Id<User> userId)
    {
        var tasks = await getTasksQuery.RunAsync(userId);
        return tasks;
    }
}