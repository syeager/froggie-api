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
            Email = Valid.Users.Email.Value,
            Name = Valid.Users.Name.Value,
            Password = Valid.Users.Password.Value
        };

        var response = await controller.Create(request);

        ApiAssert.IsSuccess(response);
    }

    [Test]
    public void RegisterNewUser_Failure()
    {
        var request = new CreateUserRequest
        {
            Email = Valid.Users.Email.Value,
            Name = Valid.Users.Name.Value,
            Password = ""
        };

        Assert.ThrowsAsync<BadRequestException>(() => controller.Create(request).AsTask());
    }
}