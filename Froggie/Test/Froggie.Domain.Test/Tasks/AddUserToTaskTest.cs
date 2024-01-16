using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using LittleByte.Domain;

namespace Froggie.Domain.Test.Tasks;

public sealed class AddUserToTaskTest : UnitTest
{
    private AddUserToTaskService testObj = null!;
    private IFindByIdQuery<Task> findTaskQuery = null!;
    private IIsUserInGroupQuery isUserInGroupQuery = null!;

    [SetUp]
    public void SetUp()
    {
        findTaskQuery = Substitute.For<IFindByIdQuery<Task>>();

        isUserInGroupQuery = Substitute.For<IIsUserInGroupQuery>();
        isUserInGroupQuery.QueryAsync(default, default).ReturnsForAnyArgs(true);

        testObj = new AddUserToTaskService(findTaskQuery, isUserInGroupQuery);
    }

    [Test]
    public async ValueTask AddUserToTask()
    {
        var user = Valid.Users.New();
        var group = Valid.Groups.New();
        var task = Valid.Tasks.New(user, group);
        findTaskQuery.FindRequiredForEditAsync(task).Returns(task);

        await testObj.AddAsync(user, task);

        Assert.That(task.Assignees, Contains.Item(user.Id));
    }

    [Test]
    public void UserIsNotInTaskGroup()
    {
        isUserInGroupQuery.QueryAsync(default, default).ReturnsForAnyArgs(false);

        var user = Valid.Users.New();
        var group = Valid.Groups.New();
        var task = Valid.Tasks.New(user, group);
        findTaskQuery.FindRequiredForEditAsync(task).Returns(task);

        Assert.ThrowsAsync<UserNotInGroupException>(() => testObj.AddAsync(user, task).AsTask());
    }
}