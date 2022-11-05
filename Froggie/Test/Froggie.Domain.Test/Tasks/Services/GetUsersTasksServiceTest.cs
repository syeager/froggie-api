using Froggie.Domain.Tasks;
using LittleByte.Common.Infra.Models;

namespace Froggie.Domain.Test.Tasks.Services;

public sealed class GetUsersTasksServiceTest : UnitTest
{
    private IGetTasksByUserQuery getTasksQuery = null!;
    private GetUsersTasksService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        getTasksQuery = Substitute.For<IGetTasksByUserQuery>();
        testObj = new GetUsersTasksService(getTasksQuery);
    }

    [Test]
    public async ValueTask With_ValidData_Return_Tasks()
    {
        var user = Valid.Users.New();
        var tasks = Valid.Tasks.New(2);
        getTasksQuery.RunAsync(user.Id).Returns(new PageResponse<Task>(0, 0, 0, 0, tasks));

        var response = await testObj.FindAsync(user.Id);

        Assert.IsNotNull(response);
        await getTasksQuery.Received(1).RunAsync(user.Id);
        CollectionAssert.AreEqual(tasks, response.Results);
    }
}