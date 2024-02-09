using Froggie.Api.Accounts;
using Froggie.Data.Accounts;
using Froggie.Domain.Test;
using LittleByte.AspNet.Test;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Accounts;

public sealed class LogInTest : ApiIntegrationTest<LogInUserController>
{
    [Test]
    public async ValueTask LogInUser_Success()
    {
        var registerService = services.GetRequiredService<IAccountRegisterService>();
        await registerService.RegisterAsync(
            Data.Test.Valid.Accounts.Email,
            Valid.Users.Name,
            Data.Test.Valid.Accounts.Password);

        var request = new LogInUserRequest
        {
            Email = Data.Test.Valid.Accounts.Email,
            Password = Data.Test.Valid.Accounts.Password
        };

        var response = await controller.LogIn(request);

        ApiAssert.IsSuccess(response);
    }

    [Test]
    public async ValueTask LogInUser_Failure()
    {
        var request = new LogInUserRequest
        {
            Email = Data.Test.Valid.Accounts.Email,
            Password = Data.Test.Valid.Accounts.Password
        };

        var response = await controller.LogIn(request);

        ApiAssert.IsFailure(response);
    }
}