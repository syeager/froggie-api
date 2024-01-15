using Froggie.Api.Users;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.AspNet.Test;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Users;

public sealed class LogInTest : ApiIntegrationTest<LogInUserController>
{
    [Test]
    public async ValueTask LogInUser_Success()
    {
        var registerService = services.GetRequiredService<IUserRegisterService>();
        await registerService.RegisterAsync(
            Valid.Users.Email,
            Valid.Users.Name,
            Valid.Users.Password);

        var request = new LogInUserRequest
        {
            Email = Valid.Users.Email,
            Password = Valid.Users.Password
        };

        var response = await controller.LogIn(request);

        ApiAssert.IsSuccess(response);
    }

    [Test]
    public async ValueTask LogInUser_Failure()
    {
        var request = new LogInUserRequest
        {
            Email = Valid.Users.Email,
            Password = Valid.Users.Password
        };

        var response = await controller.LogIn(request);

        ApiAssert.IsFailure(response);
    }
}