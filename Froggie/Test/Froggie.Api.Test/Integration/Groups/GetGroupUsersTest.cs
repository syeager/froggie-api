using Froggie.Api.Groups;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class GetGroupUsersTest : ApiIntegrationTest<GetUsersGroupController>
{
    [Test]
    public async ValueTask QueryAllUsers()
    {
        const int userCount = 3;
        var group = await CreateGroupAndUsersHelper.CreateAsync(services, "group", userCount);
        _ = await CreateGroupAndUsersHelper.CreateAsync(services, "group-other", 2);
        await saveCommand.CommitChangesAsync();

        var users = await controller.GetUsers(group);

        ApiAssert.IsSuccess(users);
        Assert.AreEqual(userCount, users.Obj!.TotalResults);
    }
}