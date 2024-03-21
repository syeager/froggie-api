using Froggie.Data.Tasks;
using Froggie.Test;
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

    [TearDown]
    public override void TearDown()
    {
        base.TearDown();

        froggieDb.Dispose();
    }

    [Test]
    public async ValueTask When_NotAllTasksAreUsers_Return_OnlyUsersTasks()
    {
        var group = ValidGroup.New();
        var user = ValidUser.New();
        var userOther = ValidUser.New();
        var tasks = ValidTask.New(2, user.Id, group.Id);
        var tasksOther = ValidTask.New(3, userOther.Id, group.Id);
        froggieDb.Add(user);
        froggieDb.Add(userOther);
        froggieDb.AddRange(tasks);
        froggieDb.AddRange(tasksOther);
        await froggieDb.SaveChangesAsync();

        var result = await testObj.RunAsync(user.Id);

        Assert.That(result.Results.Count, Is.EqualTo(tasks.Count));
    }
}