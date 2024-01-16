using Froggie.Domain.Groups;
using LittleByte.AutoMapper;

namespace Froggie.Data.Groups;

public interface IGetTasksByGroupQuery
{
    ValueTask<Page<Task>> QueryAsync(Id<Group> groupId);
}

internal sealed class GetTasksByGroupQuery(FroggieDb database, IMapper mapper) : IGetTasksByGroupQuery
{
    public async ValueTask<Page<Task>> QueryAsync(Id<Group> groupId)
    {
        var daos = await database.Tasks
            .Where(t => t.GroupId == groupId.Value)
            .ToArrayAsync()
            .NoAwait();
        var tasks = mapper.MapRange<Task>(daos);
        var response = new Page<Task>(0, 0, 0, tasks.Length, tasks);

        return response;
    }
}