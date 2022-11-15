using Froggie.Domain.Groups;

namespace Froggie.Domain.Test.Groups;

public sealed class CreateGroupServiceTest : UnitTest
{
    private CreateGroupService testObj = null!;
    private IGroupFactory groupFactory = null!;
    private IAddGroupCommand addGroupCommand = null!;

    [SetUp]
    public void SetUp()
    {
        groupFactory = Substitute.For<IGroupFactory>();
        addGroupCommand = Substitute.For<IAddGroupCommand>();
        testObj = new CreateGroupService(groupFactory, addGroupCommand);
    }

    [Test]
    public async ValueTask When_ValidData_Then_CreateGroup()
    {
        var expected = Valid.Groups.New();
        groupFactory.Create(Arg.Any<Guid>(), expected.Name)
            .Returns(expected);

        var result = await testObj.CreateAsync(expected.Name);

        Assert.AreSame(expected, result);
        addGroupCommand.Received(1).Add(expected);
    }
}