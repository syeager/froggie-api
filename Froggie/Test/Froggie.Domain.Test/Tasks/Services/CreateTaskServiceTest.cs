using Froggie.Domain.Tasks;

namespace Froggie.Domain.Test.Tasks.Services;

public sealed class CreateTaskServiceTest : UnitTest
{
    private IAddTaskCommand addTaskCommand = null!;
    private ITaskFactory taskFactory = null!;
    private CreateTaskService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        addTaskCommand = Substitute.For<IAddTaskCommand>();
        taskFactory = Substitute.For<ITaskFactory>();
        testObj = new CreateTaskService(addTaskCommand, taskFactory);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateNewTask()
    {
        var creator = Valid.Users.New();
        var group = Valid.Groups.New();
        var expectedTask = Valid.Tasks.New(creator.Id, group.Id);
        taskFactory
            .Create(Arg.Any<Guid>(), expectedTask.Title, creator.Id, Valid.Tasks.DueDate, group.Id)
            .Returns(expectedTask);

        var task = await testObj.CreateAsync(expectedTask.Title, creator.Id, Valid.Tasks.DueDate, group.Id);

        Assert.AreNotEqual(Guid.Empty, task.Id.Value);
        Assert.AreSame(expectedTask, task);
        addTaskCommand.Received(1).Add(task);
    }
}