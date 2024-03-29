﻿using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.AutoMapper;

namespace Froggie.Data.Groups;

internal sealed class GetUsersGroupsQuery(FroggieDb froggieDb, IMapper mapper) : IGetUserGroupsQuery
{
    public async ValueTask<IReadOnlyCollection<Group>> QueryAsync(Id<User> userId)
    {
        var daos = await froggieDb
            .GroupUsers.AsNoTracking()
            .Include(ug => ug.Group).AsNoTracking()
            .Where(ug => ug.UserId == userId.Value)
            .Select(ug => ug.Group!)
            .ToArrayAsync();

        var groups = mapper.MapRange<Group>(daos);
        return groups;
    }
}