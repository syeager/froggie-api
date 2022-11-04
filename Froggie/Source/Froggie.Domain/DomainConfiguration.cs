using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection @this) => @this
        .AddTasks()
        .AddUsers();
}