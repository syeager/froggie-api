using FluentValidation;
using Froggie.Domain.Tasks;

namespace Froggie.Domain.Test.Tasks.Services;

public sealed class CreateTaskServiceTest : UnitTest
{
    private IAddTaskCommand addTaskCommand = null!;
    private CreateTaskService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        addTaskCommand = Substitute.For<IAddTaskCommand>();
        testObj = new CreateTaskService(addTaskCommand);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateNewTask()
    {
        var task = await testObj.CreateAsync(Valid.Tasks.Title);

        Assert.AreNotEqual(Guid.Empty, task.Id.Value);
        addTaskCommand.Received(1).Add(task);
    }

    [Test]
    public void With_InvalidData_Then_Throw()
    {
        Assert.ThrowsAsync<ValidationException>(() => testObj.CreateAsync("").AsTask());
    }
}