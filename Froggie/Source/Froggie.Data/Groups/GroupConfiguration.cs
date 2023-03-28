using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common.Infra.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Groups;

public static class GroupConfiguration
{
    public static IServiceCollection AddGroups(this IServiceCollection @this)
    {
        return @this
            .AddScoped<IAddGroupCommand, AddGroupCommand>()
            .AddScoped<IGetUsersGroupsQuery, GetUsersGroupsQuery>()
            .AddScoped<IGetUsersInGroupQuery, GetUsersInGroupQuery>()
            .AddScoped<IGetTasksByGroupQuery, GetTasksByGroupQuery>()
            .AddScoped<IUserGroupCreateCommand, UserGroupCreateCommand>()
            .AddScoped<IFindByIdQuery<Group>, FindByIdQuery<Group, GroupDao, FroggieDb>>();
    }
}