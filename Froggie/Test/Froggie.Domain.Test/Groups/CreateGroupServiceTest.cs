using Froggie.Domain.Groups;

namespace Froggie.Domain.Test.Groups;

public sealed class CreateGroupServiceTest : UnitTest
{
    private CreateGroupService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new CreateGroupService();
    }
}