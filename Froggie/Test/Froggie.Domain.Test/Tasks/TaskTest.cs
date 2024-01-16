using Froggie.Domain.Users;
using LittleByte.Common;

namespace Froggie.Domain.Test.Tasks;

public sealed class TaskTest : UnitTest
{
    private Task testObj = null!;
    private Id<User> userId;

    [SetUp]
    public void SetUp()
    {
        testObj = Valid.Tasks.New(Guid.NewGuid(), Guid.NewGuid());

        userId = new Id<User>();
    }

    [Test]
    public void AddNewAssignee()
    {
        testObj.AddAssignee(userId);

        Assert.That(testObj.Assignees, Contains.Item(userId));
    }

    [Test]
    public void AddExistingAssignee()
    {
        testObj.AddAssignee(userId);

        testObj.AddAssignee(userId);

        Assert.That(testObj.Assignees.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddEmptyAssignee()
    {
        Assert.Throws<ArgumentNullException>(() => testObj.AddAssignee(Id<User>.Empty));
    }

    [Test]
    public void RemoveAssignee()
    {
        testObj.AddAssignee(userId);

        testObj.RemoveAssignee(userId);

        Assert.That(testObj.Assignees.Count, Is.EqualTo(0));
    }

    [Test]
    public void RemoveEmptyUser()
    {
        Assert.Throws<ArgumentNullException>(() => testObj.RemoveAssignee(Id<User>.Empty));
    }

    [Test]
    public void RemoveMissingAssignee()
    {
        Assert.DoesNotThrow(() => testObj.RemoveAssignee(userId));
    }
}