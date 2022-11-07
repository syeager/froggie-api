using Froggie.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Test;

public abstract class DataIntegrationTest : IntegrationTest
{
    protected sealed override void SetupInternal(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddDomain()
            .AddPersistence();
    }

    [TearDown]
    public override void TearDown()
    {
        services.GetService<FroggieDb>()?.Database.EnsureDeleted();

        base.TearDown();
    }
}