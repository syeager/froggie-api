using Froggie.Api.Groups;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class GetGroupUsersTest : ApiIntegrationTest<GetGroupUsersController>
{
    [Test]
    public async ValueTask QueryAllUsers()
    {
        const int userCount = 3;
        var group = CreateGroupAndUsersHelper.Create(services, "group", userCount);
        CreateGroupAndUsersHelper.Create(services);
        await saveCommand.CommitChangesAsync();

        var users = await controller.GetUsers(group);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(users);
            Assert.That(users.Obj!.TotalResults, Is.EqualTo(userCount));
        });
    }
}