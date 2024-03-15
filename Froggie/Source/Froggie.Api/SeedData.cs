namespace Froggie.Api;

public static class SeedData
{
    public static async Task<IApplicationBuilder> AddSeedDataAsync(this IApplicationBuilder @this, IServiceProvider serviceProvider)
    {
        await Admin.Accounts.SeedData.AddSeedDataAsync(serviceProvider);
        return @this;
    }
}