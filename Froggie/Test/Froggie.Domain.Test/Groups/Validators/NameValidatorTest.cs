using Froggie.Domain.Groups.Models;
using Froggie.Domain.Groups.Validators;

namespace Froggie.Domain.Test.Groups.Validators;

public sealed class NameValidatorTest : UnitTest
{
    private NameValidator<Name> testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new NameValidator<Name>();
    }
}