using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Domain;

namespace Froggie.Domain.Test.Tasks;

public sealed class AddTaskAssigneeServiceTest : UnitTest
{
    private IFindByIdQuery<Task> findTaskQuery = null!;
    private IFindByIdQuery<User> findUserQuery = null!;
    private IIsUserInGroupQuery isUserInGroupQuery = null!;
    private AddTaskAssigneeService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        findTaskQuery = Substitute.For<IFindByIdQuery<Task>>();
        findUserQuery = Substitute.For<IFindByIdQuery<User>>();
        isUserInGroupQuery = Substitute.For<IIsUserInGroupQuery>(); 
        isUserInGroupQuery.QueryAsync(default, default).ReturnsForAnyArgs(true);

        testObj = new AddTaskAssigneeService(findTaskQuery, findUserQuery, isUserInGroupQuery);
    }

    [Test]
    public async ValueTask AddUserToTask()
    {
        var user = Valid.Users.New();
        var group = Valid.Groups.New();
        var task = Valid.Tasks.New(user, group);
        findTaskQuery.FindRequiredAsync(task).Returns(task);
        findUserQuery.FindRequiredAsync(user).Returns(user);

        var result = await testObj.AddAsync(user, task);

        Assert.Multiple(() =>
        {
            Assert.That(result.Succeeded);
            Assert.That(task.Assignees, Contains.Item(user));
        });
    }

    [Test]
    public async ValueTask UserIsNotInTaskGroup()
    {
        isUserInGroupQuery.QueryAsync(default, default).ReturnsForAnyArgs(false);

        var user = Valid.Users.New();
        var group = Valid.Groups.New();
        var task = Valid.Tasks.New(user, group);
        findTaskQuery.FindRequiredAsync(task).Returns(task);

        var result = await testObj.AddAsync(user, task);

        Assert.That(result, Is.TypeOf<UserNeedsToBeInTaskGroupToBeAssignee>());
    }
}