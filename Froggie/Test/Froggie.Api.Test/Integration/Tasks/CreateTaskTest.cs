using System.Net;
using Froggie.Api.Tasks;
using Froggie.Domain.Groups;
using Froggie.Test;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class CreateTaskTest : ApiIntegrationTest<CreateTaskController>
{
    [Test]
    public async ValueTask CreateTask_Success()
    {
        var group = ValidGroup.New();
        var user = ValidUser.New();

        var userGroupCreateCommand = GetService<IUserGroupCreateCommand>();
        userGroupCreateCommand.Create(user, group);

        await saveCommand.CommitChangesAsync();

        var request = new CreateTaskRequest
        {
            Title = ValidTask.Title,
            CreatorId = user.Id,
            DueDate = ValidTask.DueDate,
            GroupId = group.Id
        };

        var response = await controller.Create(request);

        Assert.Multiple(() =>
        {
            ApiAssert.IsSuccess(response, HttpStatusCode.Created);
            Assert.That(response.Obj!.GroupId, Is.EqualTo(group.Id.Value));
        });
    }
}