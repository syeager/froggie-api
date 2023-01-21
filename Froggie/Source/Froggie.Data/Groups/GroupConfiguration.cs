using Froggie.Domain.Groups;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Groups;

public static class GroupConfiguration
{
    public static IServiceCollection AddGroups(this IServiceCollection @this)
    {
        return @this
            .AddScoped<IAddGroupCommand, AddGroupCommand>();
    }
}