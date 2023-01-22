using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common.Domain;
using LittleByte.Test.Validation;

namespace Froggie.Domain.Test.Tasks.Validators;

public sealed class TaskValidatorTest : UnitTest
{
    private TaskValidator testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new TaskValidator();
    }

    [Test]
    public void When_Created_Then_AllMembersWithValidatorsHaveThem()
    {
        var validatedMembers = testObj.CreateDescriptor().GetMembersWithValidators();

        Assert.IsTrue(validatedMembers.Any(group => group.Key == nameof(Title)));
    }

    [Test]
    public void When_InvalidCreatorId_Then_Fail()
    {
        var task = Valid.Tasks.New(Guid.Empty, new Id<Group>());

        var result = testObj.Validate(task);

        result.AssertFailure(nameof(Task.CreatorId));
    }

    [TestCase("1/1/2000")]
    [TestCase("1/1/3000")]
    public void With_AnyDueDatePastMin_Then_Pass(string dueDateString)
    {
        var dueDate = DateTime.Parse(dueDateString);
        var task = Task.Create(
            Validator.WillPass<Task>(),
            new Id<Task>(),
            Valid.Tasks.Title,
            new Id<User>(),
            dueDate,
            new Id<Group>());

        var result = testObj.Validate(task);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void With_MinDueDate_Then_Fail()
    {
        var task = Task.Create(
            Validator.WillPass<Task>(),
            new Id<Task>(),
            Valid.Tasks.Title,
            new Id<User>(),
            DateTime.MinValue,
            new Id<Group>());

        var result = testObj.Validate(task);

        result.AssertFailure(nameof(Task.DueDate));
    }
}