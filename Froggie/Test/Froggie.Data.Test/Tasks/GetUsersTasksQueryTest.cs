using Froggie.Data.Tasks;
using LittleByte.Data;

namespace Froggie.Data.Test.Tasks;

public sealed class GetUserTasksQueryTest : UnitTest
{
    private IGetTasksByUserQuery getTasksQuery = null!;
    private GetUserTasksQuery testObj = null!;

    [SetUp]
    public void SetUp()
    {
        getTasksQuery = Substitute.For<IGetTasksByUserQuery>();
        testObj = new GetUserTasksQuery(getTasksQuery);
    }

    [Test]
    public async ValueTask With_ValidData_Return_Tasks()
    {
        var user = Domain.Test.Valid.Users.New();
        var group = Domain.Test.Valid.Groups.New();
        var tasks = Domain.Test.Valid.Tasks.New(2, user.Id, group.Id);
        getTasksQuery.RunAsync(user.Id).Returns(new Page<Task>(0, 0, 0, 0, tasks));

        var response = await testObj.FindAsync(user.Id);

        await getTasksQuery.Received(1).RunAsync(user.Id);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Results, Is.EqualTo(tasks));
            Assert.That(response.Results.All(t => t.CreatorId == user.Id), Is.True);
        });
    }
}