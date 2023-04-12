using Froggie.Api.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.Common.Domain;
using LittleByte.Common.Exceptions;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Users;

public sealed class RegisterUserTest : ApiIntegrationTest<CreateUserController>
{
    [Test]
    public async ValueTask RegisterNewUser_Success()
    {
        var request = new CreateUserRequest
        {
            Email = Valid.Users.Email,
            Name = Valid.Users.Name,
            Password = Valid.Users.Password
        };

        var response = await controller.Create(request);

        ApiAssert.IsSuccess(response);

        var userId = new Id<User>(response.Obj!.User!.Id);
        var userGroups = await GetService<IGetUsersGroupsQuery>().QueryAsync(userId);

        Assert.AreEqual(1, userGroups.Count);
        Assert.AreEqual(NameRules.PersonalName, userGroups.First().Name.Value);
    }

    [Test]
    public void RegisterNewUser_Failure()
    {
        var request = new CreateUserRequest
        {
            Email = Valid.Users.Email,
            Name = Valid.Users.Name,
            Password = ""
        };

        Assert.ThrowsAsync<BadRequestException>(() => controller.Create(request).AsTask());
    }
}