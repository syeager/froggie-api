using Froggie.Domain.Groups;
using LittleByte.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Groups;

public static class GroupConfiguration
{
    public static IServiceCollection AddGroupsData(this IServiceCollection @this) => @this
        .AddScoped<IAddGroupCommand, AddGroupCommand>()
        .AddScoped<IGetUserGroupsQuery, GetUsersGroupsQuery>()
        .AddScoped<IGetUsersInGroupQuery, GetUsersInGroupQuery>()
        .AddScoped<IFindByIdQuery<Group>, FindByIdQuery<Group, FroggieDb>>()
        .AddScoped<IGetTasksByGroupQuery, GetTasksByGroupQuery>()
        .AddScoped<IUserGroupCreateCommand, UserGroupCreateCommand>();
}