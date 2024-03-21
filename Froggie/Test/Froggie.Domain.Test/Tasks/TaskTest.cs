using Froggie.Test;

namespace Froggie.Domain.Test.Tasks;

public class TaskTest
{
    private Task testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = ValidTask.New(Guid.NewGuid(), Guid.NewGuid());
    }

    [Test]
    public void AddNewAssignee()
    {
        var user = ValidUser.New();

        testObj.AddAssignee(user);

        Assert.That(testObj.Assignees, Contains.Item(user));
    }

    [Test]
    public void AddExistingAssignee()
    {
        var user = ValidUser.New();
        testObj.AddAssignee(user);

        testObj.AddAssignee(user);

        Assert.That(testObj.Assignees.Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveAssignee()
    {
        var user = ValidUser.New();
        testObj.AddAssignee(user);

        testObj.RemoveAssignee(user);

        Assert.That(testObj.Assignees.Count, Is.EqualTo(0));
    }

    [Test]
    public void When_Completing_Then_SetToComplete()
    {
        testObj.Complete();

        Assert.That(testObj.IsCompleted);
    }
}