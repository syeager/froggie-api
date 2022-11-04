using Froggie.Api.Users;
using Froggie.Domain.Test;
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