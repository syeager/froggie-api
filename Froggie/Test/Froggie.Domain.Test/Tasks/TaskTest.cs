namespace Froggie.Domain.Test.Tasks;

public class TaskTest
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
        var user = Valid.Users.New();

        testObj.AddAssignee(user);

        Assert.That(testObj.Assignees, Contains.Item(user));
    }

    [Test]
    public void Given_ExistingAssignee_Then_DoNotAdd()
    {
        var user = Valid.Users.New();
        testObj.AddAssignee(user);

        testObj.AddAssignee(user);

        Assert.That(testObj.Assignees.Count, Is.EqualTo(1));
    }
}