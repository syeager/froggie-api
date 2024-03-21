using System.Net;
using Froggie.Api.Groups;
using Froggie.Data;
using Froggie.Data.Accounts;
using Froggie.Test;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class CreateGroupTest : ApiIntegrationTest<CreateGroupController>
{
    [Test]
    public async ValueTask CreateGroup_Success()
    {
        var result = await GetService<IAccountRegisterService>()
            .RegisterAsync(ValidAccount.Email, ValidUser.Name, ValidAccount.Password);

        await saveCommand.CommitChangesAsync();

        var request = new CreateGroupRequest
        {
            Name = "Group A",
            CreatorId = result.Value!.Id,
        };

        var response = await controller.Create(request);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(response, HttpStatusCode.Created);
            Assert.That(response.Obj!.Name, Is.EqualTo(request.Name));
            // There will be 2 groups because registering a user creates a personal group.
            Assert.That(GetService<FroggieDb>().Groups.Count(), Is.EqualTo(2));
        });
    }
}
