namespace Froggie.Api;

public static class SeedData
{
    public static async Task<IApplicationBuilder> AddSeedDataAsync(this IApplicationBuilder @this, IServiceProvider services)
    {
        await Data.SeedData.AddSeedDataAsync(services);
        return @this;
    }
}