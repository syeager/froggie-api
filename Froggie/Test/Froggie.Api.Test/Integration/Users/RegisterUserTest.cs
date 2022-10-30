using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Froggie.Api.Users;
using Froggie.Data;
using Froggie.Domain;
using LittleByte.Common.Identity.Services;
using LittleByte.Common.Logging;
using LittleByte.Common.Logging.Configuration;
using LittleByte.Test.Categories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Froggie.Api.Test.Integration.Users;

public class NullIdentity : ITokenGenerator
{
    public JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims)
    {
        return new JwtSecurityToken();
    }
}

public class NullDisposable : IDisposable
{
    public void Dispose() { }
}

public class NullLogger<T> : ILogger<T>
{
    public IDisposable BeginScope<TState>(TState state)
    {
        return new NullDisposable();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel,
                            EventId eventId,
                            TState state,
                            Exception? exception,
                            Func<TState, Exception?, string> formatter) { }
}

public sealed class RegisterUserTest : IntegrationTest
{
    private ServiceProvider services = null!;

    [SetUp]
    public void SetUp()
    {
        var serviceCollection = new ServiceCollection()
            .AddDomain()
            .AddPersistence()
            .AddTransient(typeof(ILogger<>), typeof(NullLogger<>))
            .AddTransient<SecurityTokenHandler, JwtSecurityTokenHandler>()
            .AddScoped<IDiagnosticContext, NullDiagnosticContext>()
            .AddLogs()
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddTransient<ITokenGenerator, NullIdentity>()
            .AddTransient<CreateUserController>();

        services = serviceCollection.BuildServiceProvider();
    }

    [Test]
    public async ValueTask RegisterNewUser()
    {
        var controller = services.GetRequiredService<CreateUserController>();
        // TODO: Use Valid?
        var request = new CreateUserRequest
        {
            Email = "user@example.com",
            Name = "user",
            Password = "abc123"
        };

        var response = await controller.Create(request);

        Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Obj);
        Assert.IsFalse(response.IsError);
    }
}