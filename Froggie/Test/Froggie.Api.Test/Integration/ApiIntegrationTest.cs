using Froggie.Data;
using Froggie.Domain;
using LittleByte.Test.Categories;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration;

public abstract class ApiIntegrationTest : IntegrationTest
{
    protected abstract void AddServices(IServiceCollection serviceCollection);

    protected sealed override void SetupInternal(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddDomain()
            .AddPersistence();

        AddServices(serviceCollection);
    }

    [TearDown]
    public override void TearDown()
    {
        services.GetService<FroggieDb>()?.Database.EnsureDeleted();

        base.TearDown();
    }
}