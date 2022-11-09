using System.Net;
using FluentValidation;
using Froggie.Api.Tasks;
using Froggie.Domain.Test;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class CreateTaskTest : ApiIntegrationTest<CreateTaskController>
{
    [Test]
    public async ValueTask CreateTask_Success()
    {
        var user = Valid.Users.New();
        await saveCommand.CommitChangesAsync();
        var request = new CreateTaskRequest
        {
            Title = Valid.Tasks.Title,
            CreatorId = user.Id,
        };

        var response = await controller.Create(request);

        ApiAssert.IsSuccess(response, HttpStatusCode.Created);
    }

    [Test]
    public void CreateTask_Failure()
    {
        var request = new CreateTaskRequest { Title = "" };

        Assert.ThrowsAsync<ValidationException>(() => controller.Create(request).AsTask());
    }
}