﻿using Froggie.Data.Tasks;
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
        var group = Domain.Test.Valid.Groups.New();
        var user = Domain.Test.Valid.Users.New();
        var userOther = Domain.Test.Valid.Users.New();
        var tasks = Domain.Test.Valid.Tasks.New(2, user.Id, group.Id);
        var tasksOther = Domain.Test.Valid.Tasks.New(3, userOther.Id, group.Id);
        froggieDb.Add(user);
        froggieDb.Add(userOther);
        froggieDb.AddRange(tasks);
        froggieDb.AddRange(tasksOther);
        await froggieDb.SaveChangesAsync();

        var result = await testObj.RunAsync(user.Id);

        Assert.That(result.Results.Count, Is.EqualTo(tasks.Count));
    }
}