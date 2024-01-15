using Froggie.Domain.Groups;

namespace Froggie.Domain.Test.Groups;

public sealed class NameValidatorTest : UnitTest
{
    private NameValidator<Name> testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new NameValidator<Name>();
    }

    [TestCase(NameRules.LengthMax)]
    [TestCase(NameRules.LengthMin)]
    public void With_ValidData_Then_Pass(int length)
    {
        var name = new Name(new string('a', length));

        var result = testObj.IsValid(null!, name);

        Assert.That(result);
    }

    [TestCase(NameRules.LengthMax + 1)]
    [TestCase(NameRules.LengthMin - 1)]
    public void With_InvalidData_Then_Fail(int length)
    {
        var name = new Name(new string('a', length));

        var result = testObj.IsValid(null!, name);

        Assert.That(result, Is.False);
    }
}