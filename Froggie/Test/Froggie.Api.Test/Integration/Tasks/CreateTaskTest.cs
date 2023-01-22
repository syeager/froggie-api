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
        var group = Valid.Groups.New();

        var user = Valid.Users.New();
        await saveCommand.CommitChangesAsync();
        var request = new CreateTaskRequest
        {
            Title = Valid.Tasks.Title,
            CreatorId = user.Id,
            DueDate = Valid.Tasks.DueDate,
            GroupId = group.Id
        };

        var response = await controller.Create(request);

        ApiAssert.IsSuccess(response, HttpStatusCode.Created);
        Assert.AreEqual(group.Id.Value, response.Obj!.GroupId);
    }

    [Test]
    public void CreateTask_Failure()
    {
        var request = new CreateTaskRequest {Title = ""};

        Assert.ThrowsAsync<ValidationException>(() => controller.Create(request).AsTask());
    }
}