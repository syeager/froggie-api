using Froggie.Domain.Groups;

namespace Froggie.Domain.Test.Groups;

public sealed class NameValidatorTest : UnitTest
{
    private NameValidator<GroupName> testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new NameValidator<GroupName>();
    }

    [TestCase(GroupNameRules.LengthMax)]
    [TestCase(GroupNameRules.LengthMin)]
    public void With_ValidData_Then_Pass(int length)
    {
        var name = new GroupName(new string('a', length));

        var result = testObj.IsValid(null!, name);

        Assert.That(result);
    }

    [TestCase(GroupNameRules.LengthMax + 1)]
    [TestCase(GroupNameRules.LengthMin - 1)]
    public void With_InvalidData_Then_Fail(int length)
    {
        var name = new GroupName(new string('a', length));

        var result = testObj.IsValid(null!, name);

        Assert.That(result, Is.False);
    }
}