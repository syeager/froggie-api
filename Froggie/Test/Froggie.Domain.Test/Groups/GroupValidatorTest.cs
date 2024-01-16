using Froggie.Domain.Groups;

namespace Froggie.Domain.Test.Groups;

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

        Assert.That(descriptor.GetValidatorsForMember(nameof(Group.Name)), Is.Not.Empty);
    }
}