using FluentValidation;
using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using Froggie.Test;
using LittleByte.Common;
using LittleByte.Validation.Test;

namespace Froggie.Domain.Test.Tasks;

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

        Assert.That(validatedMembers.Any(group => group.Key == nameof(Title)));
    }

    [Test]
    public void When_InvalidCreatorId_Then_Fail()
    {
        var exception = Assert.Throws<ValidationException>(() => ValidTask.New(Guid.Empty, new Id<Group>()));

        exception!.AssertFailure(nameof(Task.CreatorId));
    }

    [TestCase("1/1/2000")]
    [TestCase("1/1/3000")]
    public void With_AnyDueDatePastMin_Then_Pass(string dueDateString)
    {
        var dueDate = DateTime.Parse(dueDateString);
        var task = Task.Create(
            new Id<Task>(),
            ValidTask.Title,
            new Id<User>(),
            dueDate,
            new Id<Group>());

        var result = testObj.Validate(task);

        Assert.That(result.IsValid);
    }

    [Test]
    public void With_MinDueDate_Then_Fail()
    {
        var exception = Assert.Throws<ValidationException>(() => Task.Create(
            new Id<Task>(),
            ValidTask.Title,
            new Id<User>(),
            DateTime.MinValue,
            new Id<Group>()));

        exception!.AssertFailure(nameof(Task.DueDate));
    }
}