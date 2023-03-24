using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common.Domain;

namespace Froggie.Domain.Test.Tasks;

public sealed class CreateTaskServiceTest : UnitTest
{
    private IAddTaskCommand addTaskCommand = null!;
    private IUserGroupExistsQuery userGroupExistsQuery = null!;
    private ITaskFactory taskFactory = null!;
    private CreateTaskService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        addTaskCommand = Substitute.For<IAddTaskCommand>();
        userGroupExistsQuery = Substitute.For<IUserGroupExistsQuery>();
        taskFactory = Substitute.For<ITaskFactory>();
        testObj = new CreateTaskService(addTaskCommand, userGroupExistsQuery, taskFactory);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateNewTask()
    {
        var creator = Valid.Users.New();
        var group = Valid.Groups.New();
        userGroupExistsQuery.QueryAsync(creator, group).Returns(true);
        var expectedTask = Valid.Tasks.New(creator, group);
        taskFactory
            .Create(default, default!, default, default, default)
            .ReturnsForAnyArgs(expectedTask);

        var task = await testObj.CreateAsync(expectedTask.Title, creator.Id, Valid.Tasks.DueDate, group.Id);

        Assert.AreNotEqual(Id<Task>.Empty, task.Id);
        Assert.AreSame(expectedTask, task);
        addTaskCommand.Received(1).Add(task);
    }

    [Test]
    public void When_UserNotInGroup_Then_Fail()
    {
        var creator = Valid.Users.New();
        var group = Valid.Groups.New();

        Assert.ThrowsAsync<UserNotInGroupException>(() => testObj.CreateAsync(
            Valid.Tasks.Title,
            creator,
            Valid.Tasks.DueDate,
            group).AsTask());
    }
}