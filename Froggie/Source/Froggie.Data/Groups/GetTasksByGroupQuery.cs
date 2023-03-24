using Froggie.Domain.Groups;
using LittleByte.Common.AspNet.AutoMapper;
using LittleByte.Common.Tasks;

namespace Froggie.Data.Groups;

internal sealed class GetTasksByGroupQuery : IGetTasksByGroupQuery
{
    private readonly FroggieDb froggieDb;
    private readonly IMapper mapper;

    public GetTasksByGroupQuery(FroggieDb froggieDb, IMapper mapper)
    {
        this.froggieDb = froggieDb;
        this.mapper = mapper;
    }

    public async ValueTask<PageResponse<Task>> QueryAsync(Id<Group> groupId)
    {
        var daos = await froggieDb.Tasks
            .Where(t => t.GroupId == groupId.Value)
            .ToArrayAsync()
            .NoAwait();
        var tasks = mapper.MapRange<Task>(daos);
        var response = new PageResponse<Task>(0, 0, 0, tasks.Length, tasks);

        return response;
    }
}