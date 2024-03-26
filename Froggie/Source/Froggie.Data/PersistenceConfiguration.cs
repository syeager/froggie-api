using LittleByte.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data;

public static class PersistenceConfiguration
{
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services
            .AddSingleton<StringValueObjectConverter>()
            .AddSingleton(TimeProvider.System)
            .AddScoped<ISaveContextCommand, SaveContextCommand<FroggieDb>>()
            .AddDbContext<FroggieDb>(options => options.UseInMemoryDatabase("froggie-core"));
    }
}