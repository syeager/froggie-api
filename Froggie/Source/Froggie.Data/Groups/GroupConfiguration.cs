using Froggie.Domain.Groups;
using LittleByte.Common.Infra.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Groups;

public static class GroupConfiguration
{
    public static IServiceCollection AddGroups(this IServiceCollection @this)
    {
        return @this
            .AddScoped<IAddGroupCommand, AddGroupCommand>()
            .AddScoped<IFindByIdQuery<Group>, FindByIdQuery<Group, GroupDao, FroggieDb>>();
    }
}