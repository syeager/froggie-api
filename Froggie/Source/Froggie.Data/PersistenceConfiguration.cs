using Froggie.Data.Tasks;
using Froggie.Data.Users;
using LittleByte.Common.Infra.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data;

public static class PersistenceConfiguration
{
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public static IServiceCollection AddPersistence(this IServiceCollection @this)
    {
        return @this
            .AddTasks()
            .AddUsers()
            .AddScoped<ISaveContextCommand, SaveContextCommand<FroggieDb>>()
            .AddDbContext<FroggieDb>(options => options.UseInMemoryDatabase("froggie-in_memory"));
    }
}