using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Groups;

internal static class GroupsConfiguration
{
    public static IServiceCollection AddGroups(this IServiceCollection @this)
    {
        return @this
            .AddTransient<IAddUserToGroupService, AddUserToGroupService>()
            .AddTransient<ICreateGroupService, CreateGroupService>()
            .AddTransient<IGroupFactory, GroupFactory>()
            .AddTransient<IModelValidator<Group>, GroupValidator>();
    }
}