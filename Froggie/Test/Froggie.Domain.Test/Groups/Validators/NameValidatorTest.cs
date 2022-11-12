using Froggie.Domain.Groups;

namespace Froggie.Domain.Test.Groups.Validators;

public sealed class NameValidatorTest : UnitTest
{
    private NameValidator<Name> testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new NameValidator<Name>();
    }

    [TestCase(0)]
    public void With_ValidData_Then_Pass(int length)
    {
        var name = new Name(new string('a', length));

        var result = testObj.IsValid(null!, name);

        Assert.IsTrue(result);
    }
}