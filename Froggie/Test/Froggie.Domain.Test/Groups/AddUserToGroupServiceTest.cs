using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Test.Groups;

public sealed class AddUserToGroupServiceTest : UnitTest
{
    private AddUserToGroupService testObj = null!;
    private IUserGroupExistsQuery existsQuery = null!;
    private IUserGroupCreateCommand createCommand = null!;

    [SetUp]
    public void SetUp()
    {
        existsQuery = Substitute.For<IUserGroupExistsQuery>();
        createCommand = Substitute.For<IUserGroupCreateCommand>();
        testObj = new AddUserToGroupService(existsQuery, createCommand);
    }

    [Test]
    public async ValueTask When_UserNotInGroup_Then_AddUser()
    {
        var user = Valid.Users.New();
        var group = Valid.Groups.New();

        var result = await testObj.AddAsync(user, group);

        Assert.That(result.Succeeded);
    }

    [Test]
    public async ValueTask When_UserInGroup_Then_Exit()
    {
        var user = Valid.Users.New();
        var group = Valid.Groups.New();
        existsQuery.QueryAsync(user.Id, group.Id).Returns(true);

        var result = await testObj.AddAsync(user, group);

        Assert.That(result, Is.TypeOf<UserAlreadyInGroup>());
    }
}