using Froggie.Domain.Groups;

namespace Froggie.Domain.Test.Groups.Validators;

public sealed class GroupValidatorTest : UnitTest
{
    private GroupValidator testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new GroupValidator();
    }

    [Test]
    public void When_Created_Then_HaveAllPropertyValidators()
    {
        var descriptor = testObj.CreateDescriptor();
            
        CollectionAssert.IsNotEmpty(descriptor.GetValidatorsForMember(nameof(Group.Name)));
    }
}