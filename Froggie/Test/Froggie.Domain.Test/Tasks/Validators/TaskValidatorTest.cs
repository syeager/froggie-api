using FluentValidation;
using Froggie.Domain.Tasks;
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
        var exception = Assert.Throws<ValidationException>(() => Valid.Tasks.New(Guid.Empty));

        exception!.AssertFailure(nameof(Task.CreatorId));
    }
}