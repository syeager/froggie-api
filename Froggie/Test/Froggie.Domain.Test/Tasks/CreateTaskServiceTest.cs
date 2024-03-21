using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using Froggie.Test;
using LittleByte.Common;

namespace Froggie.Domain.Test.Tasks;

public sealed class CreateTaskServiceTest : UnitTest
{
    private IAddTaskCommand addTaskCommand = null!;
    private CreateTaskService testObj = null!;
    private IUserGroupExistsQuery userGroupExistsQuery = null!;

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
        var creator = ValidUser.New();
        var group = ValidGroup.New();
        userGroupExistsQuery.QueryAsync(creator, group).Returns(true);
        var expectedTask = ValidTask.New(creator, group);

        var result = await testObj.CreateAsync(expectedTask.Title, creator.Id, ValidTask.DueDate, group.Id);

        Assert.That(result.Value!.Id, Is.Not.EqualTo(Id<Task>.Empty));
        addTaskCommand.Received(1).Add(result.Value!);
    }

    [Test]
    public async ValueTask When_UserNotInGroup_Then_Fail()
    {
        var creator = ValidUser.New();
        var group = ValidGroup.New();

        var result = await testObj.CreateAsync(
            ValidTask.Title,
            creator,
            ValidTask.DueDate,
            group);

        Assert.That(result, Is.TypeOf<UserNeedsToBeInGroupToCreateTask>());
    }
}