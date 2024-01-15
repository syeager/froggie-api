using LittleByte.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Groups;

public static class GroupsConfiguration
{
    public static IServiceCollection AddGroupsDomain(this IServiceCollection @this)
    {
        return @this
            .AddTransient<IAddUserToGroupService, AddUserToGroupService>()
            .AddTransient<ICreateGroupService, CreateGroupService>()
            .AddTransient<IGroupFactory, GroupFactory>()
            .AddTransient<IModelValidator<Group>, GroupValidator>();
    }
}