using LittleByte.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Groups;

public static class GroupsConfiguration
{
    public static IServiceCollection AddGroupsDomain(this IServiceCollection @this) => @this
        .AddTransient<ICreateGroupService, CreateGroupService>()
        .AddTransient<IModelValidator<Group>, GroupValidator>();
}