using Froggie.Data.Tasks;
using LittleByte.Infra.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddPersistence(this IServiceCollection @this)
    {
        return @this
            .AddTasks()
            .AddScoped<ISaveContextCommand, SaveContextCommand<FroggieDb>>()
            .AddDbContext<FroggieDb>(options => options.UseInMemoryDatabase("froggie-inmemory"));
    }
}