using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Data.Groups;

internal sealed class GetUsersGroupsQuery : IGetUsersGroupsQuery
{
    private readonly FroggieDb froggieDb;
    private readonly IMapper mapper;

    public GetUsersGroupsQuery(FroggieDb froggieDb, IMapper mapper)
    {
        this.froggieDb = froggieDb;
        this.mapper = mapper;
    }

    public async ValueTask<IReadOnlyCollection<Group>> QueryAsync(Id<User> userId)
    {
        var daos = await froggieDb
            .UserGroupMaps.AsNoTracking()
            .Include(ug => ug.Group).AsNoTracking()
            .Where(ug => ug.UserId == userId.Value)
            .Select(ug => ug.Group!)
            .ToArrayAsync();

        var groups = mapper.MapRange<Group>(daos);
        return groups;
    }
}