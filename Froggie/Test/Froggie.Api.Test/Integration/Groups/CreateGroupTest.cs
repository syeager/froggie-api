using System.Net;
using Froggie.Api.Groups;
using Froggie.Data;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class CreateGroupTest : ApiIntegrationTest<CreateGroupController>
{
    [Test]
    public async ValueTask CreateGroup_Success()
    {
        var user = await GetService<IUserRegisterService>()
            .RegisterAsync(Valid.Users.Email, Valid.Users.Name, Valid.Users.Password);

        await saveCommand.CommitChangesAsync();

        var request = new CreateGroupRequest
        {
            Name = "Group A",
            CreatorId = user.Id,
        };

        var response = await controller.Create(request);

        ApiAssert.IsSuccess(response, HttpStatusCode.Created);
        Assert.AreEqual(request.Name, response.Obj!.Name);
        // There will be 2 groups because registering a user creates a personal group.
        Assert.AreEqual(2, GetService<FroggieDb>().Groups.Count());
    }
}
