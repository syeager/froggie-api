using Froggie.Domain.Groups;
using LittleByte.Validation.Test;

namespace Froggie.Domain.Test.Groups;

public sealed class GroupFactoryTest : UnitTest
{
    private GroupFactory testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new GroupFactory(Validator.WillPass<Group>());
    }

    [Test]
    public void When_ValidData_Then_CreateGroup()
    {
        var expected = Valid.Groups.New();

        var result = testObj.Create(expected.Id, expected.Name);

        // TODO: Assert that all property values are the same.
        Assert.That(result.Name, Is.EqualTo(expected.Name));
    }
}