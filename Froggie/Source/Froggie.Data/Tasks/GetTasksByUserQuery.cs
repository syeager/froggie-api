using Froggie.Domain.Users;
using LittleByte.AutoMapper;

namespace Froggie.Data.Tasks;

public interface IGetTasksByUserQuery
{
    ValueTask<Page<Task>> RunAsync(Id<User> userId);
}

internal sealed class GetTasksByUserQuery(FroggieDb db, IMapper mapper) : IGetTasksByUserQuery
{
    public async ValueTask<Page<Task>> RunAsync(Id<User> userId)
    {
        var daos = await db.Tasks
            .Where(t => t.CreatorId == userId.Value)
            .ToArrayAsync()
            .NoAwait();
        var tasks = mapper.MapRange<Task>(daos);
        var response = new Page<Task>(0, 0, 0, 0, tasks);

        return response;
    }
}