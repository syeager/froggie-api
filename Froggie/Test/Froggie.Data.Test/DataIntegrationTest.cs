using Froggie.Data.Groups;
using Froggie.Data.Tasks;
using Froggie.Data.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Test;

public abstract class DataIntegrationTest : IntegrationTest
{
    protected sealed override void SetupInternal(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddPersistence()
            .AddUsersData()
            .AddUsersDomain()
            .AddTasksData()
            .AddTasksDomain()
            .AddGroupsData()
            .AddGroupsDomain();
    }

    [TearDown]
    public override void TearDown()
    {
        services.GetService<FroggieDb>()?.Database.EnsureDeleted();

        base.TearDown();
    }
}