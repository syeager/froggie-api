using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Test.Groups;

public sealed class CreateGroupServiceTest : UnitTest
{
    private static readonly Group ExpectedGroup = Valid.Groups.New();
    private static readonly User User = Valid.Users.New();

    private CreateGroupService testObj = null!;
    private IGroupFactory groupFactory = null!;
    private IAddGroupCommand addGroupCommand = null!;
    private IAddUserToGroupService addUserToGroupService = null!;

    [SetUp]
    public void SetUp()
    {
        groupFactory = Substitute.For<IGroupFactory>();
        groupFactory.Create(default, default!).ReturnsForAnyArgs(ExpectedGroup);
        addGroupCommand = Substitute.For<IAddGroupCommand>();
        addUserToGroupService = Substitute.For<IAddUserToGroupService>();
        testObj = new CreateGroupService(groupFactory, addGroupCommand, addUserToGroupService);
    }

    [Test]
    public async ValueTask When_ValidData_Then_CreateGroup()
    {
        var result = await testObj.CreateAsync(User, ExpectedGroup.Name);

        Assert.AreSame(ExpectedGroup, result);
        addGroupCommand.Received(1).Add(ExpectedGroup);
        await addUserToGroupService.Received(1).AddAsync(User, ExpectedGroup);
    }

    [Test]
    public async ValueTask When_CreatingPersonalGroup_Then_CreateGroup()
    {
        var groupName = "";
        groupFactory.WhenForAnyArgs(gf => gf.Create(default, null!)).Do(info => groupName = info.Arg<string>());

        await testObj.CreatePersonalAsync(User);

        Assert.AreEqual(NameRules.PersonalName, groupName);
        await addUserToGroupService.Received(1).AddAsync(User, ExpectedGroup);
    }
}