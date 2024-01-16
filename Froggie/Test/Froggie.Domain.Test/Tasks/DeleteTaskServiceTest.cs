using Froggie.Domain.Tasks;
using LittleByte.Common;

namespace Froggie.Domain.Test.Tasks;

public sealed class DeleteTaskServiceTest : UnitTest
{
    private IDeleteTaskCommand deleteCommand = null!;
    private DeleteTaskService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        deleteCommand = Substitute.For<IDeleteTaskCommand>();
        testObj = new DeleteTaskService(deleteCommand);
    }

    [Test]
    public async ValueTask With_ValidData_Then_DeleteTask()
    {
        var id = new Id<Task>();

        await testObj.DeleteAsync(id);

        await deleteCommand.Received(1).DeleteAsync(id);
    }
}