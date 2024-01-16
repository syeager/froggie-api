using Froggie.Data.Groups;
using Froggie.Domain.Groups;

namespace Froggie.Api.Groups;

public static class GroupsConfiguration
{
    public static IServiceCollection AddGroups(this IServiceCollection services) => services
        .AddGroupsDomain()
        .AddGroupsData();
}