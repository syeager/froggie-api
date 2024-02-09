using FluentValidation;
using Froggie.Api.Accounts;
using Froggie.Domain.Groups;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.AspNet.Test;
using LittleByte.Common;

namespace Froggie.Api.Test.Integration.Accounts;

public sealed class CreateAccountTest : ApiIntegrationTest<CreateAccountController>
{
    [Test]
    public async ValueTask RegisterNewUser_Success()
    {
        var request = new CreateAccountRequest
        {
            Email = Data.Test.Valid.Accounts.Email,
            Name = Valid.Users.Name,
            Password = Data.Test.Valid.Accounts.Password
        };

        var response = await controller.Create(request);

        ApiAssert.IsSuccess(response);

        var userId = new Id<User>(response.Obj!.User!.Id);
        var userGroups = await GetService<IGetUserGroupsQuery>().QueryAsync(userId);
        Assert.Multiple(() =>
        {
            Assert.That(userGroups.Count, Is.EqualTo(1));
            Assert.That(userGroups.First().Name, Is.EqualTo(GroupNameRules.PersonalName));
        });
    }

    [Test]
    public void RegisterNewUser_Failure()
    {
        var request = new CreateAccountRequest
        {
            Email = "",
            Name = "",
            Password = ""
        };

        Assert.ThrowsAsync<ValidationException>(() => controller.Create(request).AsTask());
    }
}