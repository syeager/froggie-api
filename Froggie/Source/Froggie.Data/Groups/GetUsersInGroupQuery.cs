using AutoMapper;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common.Database.Extensions;
using LittleByte.Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Data.Groups;

internal sealed class GetUsersInGroupQuery : IGetUsersInGroupQuery
{
    private readonly FroggieDb froggieDb;
    private readonly IMapper mapper;

    public GetUsersInGroupQuery(FroggieDb froggieDb, IMapper mapper)
    {
        this.froggieDb = froggieDb;
        this.mapper = mapper;
    }

    public async ValueTask<PageResponse<User>> QueryAsync(Id<Group> groupId)
    {
        // TODO: Pull from configuration.
        const int pageSize = 10;

        var dao = await froggieDb
            .UserGroupMaps.AsNoTracking()
            .Where(um => um.GroupId == groupId)
            .Include(um => um.User).AsNoTracking()
            .Select(um => um.User)
            .GetPagedAsync(pageSize, 0);

        var users = dao.CastResults<User>(mapper);
        return users;
    }
}