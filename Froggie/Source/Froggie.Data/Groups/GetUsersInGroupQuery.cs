using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Data.Groups;

public interface IGetUsersInGroupQuery
{
    public ValueTask<Page<User>> QueryAsync(Id<Group> groupId);
}

internal sealed class GetUsersInGroupQuery(FroggieDb database, IMapper mapper) : IGetUsersInGroupQuery
{
    public async ValueTask<Page<User>> QueryAsync(Id<Group> groupId)
    {
        // TODO: Pull from configuration.
        const int pageSize = 10;

        var dao = await database
            .UserGroupMaps.AsNoTracking()
            .Where(um => um.GroupId == groupId)
            .Include(um => um.User).AsNoTracking()
            .Select(um => um.User)
            .GetPagedAsync(pageSize, 0);

        var users = dao.CastResults(mapper.Map<User>);
        return users;
    }
}