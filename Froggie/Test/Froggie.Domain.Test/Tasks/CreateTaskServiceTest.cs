using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common;

namespace Froggie.Domain.Test.Tasks;

public sealed class CreateTaskServiceTest : UnitTest
{
    private IAddTaskCommand addTaskCommand = null!;
    private IUserGroupExistsQuery userGroupExistsQuery = null!;
    private CreateTaskService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        addTaskCommand = Substitute.For<IAddTaskCommand>();
        userGroupExistsQuery = Substitute.For<IUserGroupExistsQuery>();
        testObj = new CreateTaskService(addTaskCommand, userGroupExistsQuery);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateNewTask()
    {
        var creator = Valid.Users.New();
        var group = Valid.Groups.New();
        userGroupExistsQuery.QueryAsync(creator, group).Returns(true);
        var expectedTask = Valid.Tasks.New(creator, group);

        var result = await testObj.CreateAsync(expectedTask.Title, creator.Id, Valid.Tasks.DueDate, group.Id);

        Assert.That(result.Value!.Id, Is.Not.EqualTo(Id<Task>.Empty));
        addTaskCommand.Received(1).Add(result.Value!);
    }

    [Test]
    public async ValueTask When_UserNotInGroup_Then_Fail()
    {
        var creator = Valid.Users.New();
        var group = Valid.Groups.New();

        var result = await testObj.CreateAsync(
            Valid.Tasks.Title,
            creator,
            Valid.Tasks.DueDate,
            group);

        Assert.That(result, Is.TypeOf<UserNeedsToBeInGroupToCreateTask>());
    }
}