using Froggie.Domain.Users;
using LittleByte.Common.Domain;

namespace Froggie.Domain.Test.Tasks;

public sealed class TaskTest : UnitTest
{
    private Task testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = Valid.Tasks.New(Guid.NewGuid(), Guid.NewGuid());
    }

    [Test]
    public void Given_Assignee_Then_AddAssignee()
    {
        var userId = new Id<User>();

        testObj.AddAssignee(userId);

        CollectionAssert.Contains(testObj.Assignees, userId);
    }

    [Test]
    public void Given_ExistingAssignee_Then_DoNotAdd()
    {
        var userId = new Id<User>();
        testObj.AddAssignee(userId);

        testObj.AddAssignee(userId);

        Assert.AreEqual(1, testObj.Assignees.Count);
    }

    [Test]
    public void Given_EmptyAssignee_Then_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => testObj.AddAssignee(Id<User>.Empty));
    }
}