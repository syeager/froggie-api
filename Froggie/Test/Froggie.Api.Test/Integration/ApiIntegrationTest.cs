using Froggie.Data;
using Froggie.Domain;
using LittleByte.Common.AspNet.Core;
using LittleByte.Test.Categories;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration;

public abstract class ApiIntegrationTest<T> : IntegrationTest
    where T : Controller
{
    protected T controller = null!;

    protected sealed override void SetupInternal(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddDomain()
            .AddPersistence()
            .AddTransient<T>();
    }

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        controller = services.GetRequiredService<T>();
    }

    [TearDown]
    public override void TearDown()
    {
        services.GetService<FroggieDb>()?.Database.EnsureDeleted();

        base.TearDown();
    }
}