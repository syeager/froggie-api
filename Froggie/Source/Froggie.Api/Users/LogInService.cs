using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Froggie.Domain.Users;

namespace Froggie.Api.Users;

public interface ILogInService
{
    public Task<LogInResult> LogInAsync(Email email, Password password);
}

internal sealed class LogInService(
        ITokenGenerator tokenGenerator,
        IFindUserByEmailAndPasswordQuery findUserByEmailAndPasswordQuery)
    : ILogInService
{
    public async Task<LogInResult> LogInAsync(Email email, Password password)
    {
        LogInResult result;

        var user = await findUserByEmailAndPasswordQuery.TryFindAsync(email, password);
        if(user is not null)
        {
            var claims = GetUserClaims(user);
            var token = tokenGenerator.GenerateJwt(claims);
            result = LogInResult.Success(token, user);
        }
        else
        {
            result = LogInResult.Fail(new[] { "Incorrect email or password." });
        }

        return result;
    }

    private static IReadOnlyList<Claim> GetUserClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Name.ToString())
        };
        return claims;
    }
}