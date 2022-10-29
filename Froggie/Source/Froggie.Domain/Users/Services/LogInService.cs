using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LittleByte.Common.Identity.Services;

namespace Froggie.Domain.Users;

public interface ILogInService
{
    public Task<LogInResult> LogInAsync(Email email, Password password);
}

internal sealed class LogInService : ILogInService
{
    private readonly IFindUserByEmailAndPasswordQuery findUserByEmailAndPasswordQuery;
    private readonly ITokenGenerator tokenGenerator;

    public LogInService(
        ITokenGenerator tokenGenerator,
        IFindUserByEmailAndPasswordQuery findUserByEmailAndPasswordQuery)
    {
        this.tokenGenerator = tokenGenerator;
        this.findUserByEmailAndPasswordQuery = findUserByEmailAndPasswordQuery;
    }

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
            result = LogInResult.Fail(new[] {"Incorrect email or password."});
        }

        return result;
    }

    private static IEnumerable<Claim> GetUserClaims(User user)
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
