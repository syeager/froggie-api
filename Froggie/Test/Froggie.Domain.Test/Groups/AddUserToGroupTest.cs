using Froggie.Domain.Groups;
using Froggie.Test;

namespace Froggie.Domain.Test.Groups;

public sealed class AddUserToGroupTest : UnitTest
{
    [Test]
    public void AddNewUserToGroup()
    {
        var user = ValidUser.New();
        var group = ValidGroup.New();
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
        var user = ValidUser.New();
        var group = ValidGroup.New();
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