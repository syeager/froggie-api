using Froggie.Accounts;
using Froggie.Api.Accounts;
using Froggie.Api.Groups;
using Froggie.Api.Tasks;
using Froggie.Api.Users;
using Froggie.Data;
using LittleByte.AspNet;
using LittleByte.EntityFramework;
using LittleByte.Test.Categories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration;

public abstract class ApiIntegrationTest<T> : IntegrationTest
    where T : Controller
{
    protected T controller = null!;
    protected ISaveContextCommand saveCommand = null!;

    protected sealed override void ConfigureInternal(ConfigurationBuilder builder)
    {
        builder.AddJsonFile("appsettings.Development.json");
    }

    protected sealed override void SetupInternal(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddAccounts(configuration)
            .AddUsers()
            .AddTasks()
            .AddGroups()
            .AddPersistence()
            .AddTransient<T>();
    }

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        controller = GetService<T>();
        saveCommand = GetService<ISaveContextCommand>();
    }

    [TearDown]
    public override void TearDown()
    {
        GetService<FroggieDb>().Database.EnsureDeleted();
        GetService<AccountsDb>().Database.EnsureDeleted();

        base.TearDown();
    }
}