using Froggie.Api.Groups;
using Froggie.Domain.Users;
using Froggie.Test;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class AddUserToGroupTest : ApiIntegrationTest<AddUserToGroupController>
{
    [Test]
    public async ValueTask AddNewUserToGroup()
    {
       var group = CreateGroupAndUsersHelper.Create(services);
       var user = ValidUser.New(ValidUser.Name2);
       GetService<IAddUserCommand>().Add(user);
       await saveCommand.CommitChangesAsync();

       var request = new AddUserToGroupRequest(user, group);
       var response = await controller.AddUser(request);

       ApiAssert.IsSuccess(response);
    }
}