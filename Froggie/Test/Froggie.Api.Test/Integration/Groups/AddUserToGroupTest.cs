using Froggie.Api.Groups;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.AspNet.Test;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class AddUserToGroupTest : ApiIntegrationTest<AddUserToGroupController>
{
    [Test]
    public async ValueTask AddNewUserToGroup()
    {
       var group = CreateGroupAndUsersHelper.Create(services);
       var user = Valid.Users.New(Valid.Users.Name2);
       GetService<IAddUserCommand>().Add(user);
       await saveCommand.CommitChangesAsync();

       var request = new AddUserToGroupRequest(user, group);
       var response = await controller.AddUser(request);

       ApiAssert.IsSuccess(response);
    }
}