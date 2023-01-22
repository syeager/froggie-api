using System.Net;
using Froggie.Api.Groups;
using Froggie.Data;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class CreateGroupTest : ApiIntegrationTest<CreateGroupController>
{
    [Test]
    public async ValueTask CreateTask_Success()
    {
        var request = new CreateGroupRequest
        {
            Name = "Group A"
        };

        var response = await controller.Create(request);

        ApiAssert.IsSuccess(response, HttpStatusCode.Created);
        Assert.AreEqual(request.Name, response.Obj!.Name);
        Assert.AreEqual(1, GetService<FroggieDb>().Groups.Count());
    }
}