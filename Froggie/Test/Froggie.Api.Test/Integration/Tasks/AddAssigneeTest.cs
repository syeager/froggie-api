using Froggie.Api.Tasks;
using Froggie.Api.Test.Integration.Groups;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class AddAssigneeTest : ApiIntegrationTest<AddAssigneeController>
{
    [Test]
    public async ValueTask AddNewAssignee()
    {
        var (group, users) = await CreateGroupAndUsersHelper.CreateAsync(services, userCount: 1);
        await saveCommand.CommitChangesAsync();
        var userId = users.First().Id;

        var response = await controller.AddAssignee();

        ApiAssert.IsSuccess(response);
        CollectionAssert.Contains(response.Obj!.Assignees, userId);
    }
}