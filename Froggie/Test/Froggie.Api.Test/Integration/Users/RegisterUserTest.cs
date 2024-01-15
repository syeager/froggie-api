using FluentValidation;
using Froggie.Api.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.AspNet.Test;
using LittleByte.Common;

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

        Assert.Multiple(() =>
        {
            Assert.That(userGroups.Count, Is.EqualTo(1));
            Assert.That(userGroups.First().Name.Value, Is.EqualTo(NameRules.PersonalName));
        });
    }

    [Test]
    public void RegisterNewUser_Failure()
    {
        var request = new CreateUserRequest
        {
            Email = "",
            Name = Valid.Users.Name,
            Password = ""
        };

        Assert.ThrowsAsync<ValidationException>(() => controller.Create(request).AsTask());
    }
}