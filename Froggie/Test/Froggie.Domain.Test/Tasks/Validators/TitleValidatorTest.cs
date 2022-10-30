using Froggie.Domain.Tasks;
using LittleByte.Common;
using LittleByte.Test.Validation;

namespace Froggie.Domain.Test.Tasks.Validators;

internal sealed class TitleValidatorTest : UnitTest
{
    private TitleValidator<X> testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new TitleValidator<X>();
    }

    [Test]
    public void When_Valid_Then_Pass()
    {
        var title = new Title(Valid.Tasks.Title);

        var result = testObj.IsValid(ValidationContextUtility.Empty(), title);

        Assert.IsTrue(result);
    }
}