using Froggie.Api.Groups;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class AddUserToGroupTest : ApiIntegrationTest<AddUserToGroupController>
{
    [Test]
    public async ValueTask AddNewUserToGroup()
    {
       var group = await CreateGroupAndUsersHelper.CreateAsync(services);
       var user = Valid.Users.New();
       await GetService<IAddUserCommand>().AddAsync(user, Valid.Users.Password);
       await saveCommand.CommitChangesAsync();

       var request = new AddUserToGroupRequest(user, group);
       var response = await controller.AddUser(request);

       ApiAssert.IsSuccess(response);
    }
}