using Froggie.Domain.Tasks;
using LittleByte.Common.Infra.Queries;

namespace Froggie.Domain.Test.Tasks;

// user not in group
// task doesn't exist
// user doesn't exist
public sealed class AddUserToTaskTest : UnitTest
{
    private AddUserToTaskService testObj = null!;
    private IFindByIdQuery<Task> findTaskQuery = null!;

    [SetUp]
    public void SetUp()
    {
        findTaskQuery = Substitute.For<IFindByIdQuery<Task>>();

        testObj = new AddUserToTaskService(findTaskQuery);
    }

    [Test]
    public async ValueTask AddUserToTask()
    {
        var user = Valid.Users.New();
        var group = Valid.Groups.New();
        var task = Valid.Tasks.New(user, group);
        findTaskQuery.FindRequiredForEditAsync(task).Returns(task);

        await testObj.AddAsync(user, task);

        CollectionAssert.Contains(task.Assignees, user.Id);
    }
}