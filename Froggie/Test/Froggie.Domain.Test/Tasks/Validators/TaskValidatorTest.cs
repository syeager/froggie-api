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
        var task = Valid.Tasks.New(Guid.Empty);

        var result = testObj.Validate(task);

        result.AssertFailure(nameof(Task.CreatorId));
    }
}