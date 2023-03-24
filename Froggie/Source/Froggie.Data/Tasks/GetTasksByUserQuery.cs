using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common.AspNet.AutoMapper;
using LittleByte.Common.Tasks;

namespace Froggie.Data.Tasks;

internal sealed class GetTasksByUserQuery : IGetTasksByUserQuery
{
    private readonly FroggieDb froggieDb;
    private readonly IMapper mapper;

    public GetTasksByUserQuery(FroggieDb froggieDb, IMapper mapper)
    {
        this.froggieDb = froggieDb;
        this.mapper = mapper;
    }

    public async ValueTask<PageResponse<Task>> RunAsync(Id<User> userId)
    {
        var daos = await froggieDb.Tasks
            .Where(t => t.CreatorId == userId.Value)
            .ToArrayAsync()
            .NoAwait();
        var tasks = mapper.MapRange<Task>(daos);
        var response = new PageResponse<Task>(0, 0, 0, 0, tasks);

        return response;
    }
}