using Froggie.Data.Tasks;
using Froggie.Data.Users;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Test.Tasks.Queries;

public sealed class GetTasksByUserQueryTest : DataIntegrationTest
{
    private IGetTasksByUserQuery testObj = null!;
    private FroggieDb froggieDb = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        froggieDb = services.GetRequiredService<FroggieDb>();
        testObj = services.GetRequiredService<IGetTasksByUserQuery>();
    }

    [Test]
    public async ValueTask When_NotAllTasksAreUsers_Return_OnlyUsersTasks()
    {
        var user = Valid.Users.New();
        var userOther = Valid.Users.New();
        var tasks = Valid.Tasks.New(2, user.Id);
        var tasksOther = Valid.Tasks.New(3, userOther.Id);
        froggieDb.Add<User, UserDao>(user);
        froggieDb.Add<User, UserDao>(userOther);
        froggieDb.AddRange<Task, TaskDao>(tasks);
        froggieDb.AddRange<Task, TaskDao>(tasksOther);
        await froggieDb.SaveChangesAsync();

        var result = await testObj.RunAsync(user.Id);

        Assert.AreEqual(tasks.Count, result.Results.Count);
    }
}