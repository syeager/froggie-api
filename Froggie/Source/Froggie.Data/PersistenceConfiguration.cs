using Froggie.Data.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddPersistence(this IServiceCollection @this)
    {
        return @this
            .AddTasks();
    }
}