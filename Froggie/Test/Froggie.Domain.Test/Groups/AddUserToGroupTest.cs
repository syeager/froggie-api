using Froggie.Domain.Groups;

namespace Froggie.Domain.Test.Groups;

public sealed class AddUserToGroupTest : UnitTest
{
    [Test]
    public void AddNewUserToGroup()
    {
        var user = Valid.Users.New();
        var group = Valid.Groups.New();
        var userCount = group.Users.Count;

        var result = group.AddUser(user);

        Assert.Multiple(() =>
        {
            Assert.That(result.Succeeded);
            Assert.That(group.Users.Count, Is.EqualTo(userCount + 1));
        });
    }

    [Test]
    public void UserAlreadyInGroup()
    {
        var user = Valid.Users.New();
        var group = Valid.Groups.New();
        var userCount = group.Users.Count;

        group.AddUser(user);
        var result = group.AddUser(user);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.TypeOf<UserAlreadyInGroup>());
            Assert.That(group.Users.Count, Is.EqualTo(userCount + 1));
        });
    }
}