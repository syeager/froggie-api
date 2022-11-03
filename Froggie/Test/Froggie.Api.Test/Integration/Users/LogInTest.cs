using Froggie.Api.Users;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.Test.AspNet;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Users;

public sealed class LogInTest : ApiIntegrationTest
{
    protected override void AddServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<LogInUserController>();
    }

    [Test]
    public async ValueTask LogInUser_Success()
    {
        var registerService = services.GetRequiredService<IUserRegisterService>();
        await registerService.RegisterAsync(Valid.Users.Email.Value, Valid.Users.Name.Value, Valid.Users.Password.Value);

        var controller = services.GetRequiredService<LogInUserController>();
        var request = new LogInUserRequest
        {
            Email = Valid.Users.Email.Value,
            Password = Valid.Users.Password.Value
        };

        var response = await controller.LogIn(request);

        ApiAssert.IsSuccess(response);
    }

    [Test]
    public async ValueTask LogInUser_Failure()
    {
        var controller = services.GetRequiredService<LogInUserController>();
        var request = new LogInUserRequest
        {
            Email = Valid.Users.Email.Value,
            Password = Valid.Users.Password.Value
        };

        var response = await controller.LogIn(request);

        ApiAssert.IsFailure(response);
    }
}