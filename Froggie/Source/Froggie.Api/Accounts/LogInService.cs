using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Froggie.Data.Accounts;

namespace Froggie.Api.Accounts;

public interface ILogInService
{
    public Task<LogInResult> LogInAsync(Email email, Password password);
}

internal sealed class LogInService(
        ITokenGenerator tokenGenerator,
        IFindAccountByEmailAndPassword findUserByEmailAndPasswordQuery)
    : ILogInService
{
    private static readonly string[] FailedLogIn = ["Incorrect email or password."];

    public async Task<LogInResult> LogInAsync(Email email, Password password)
    {
        LogInResult result;

        var account = await findUserByEmailAndPasswordQuery.TryFindAsync(email, password);
        if(account is not null)
        {
            var claims = GetUserClaims(account);
            var token = tokenGenerator.GenerateJwt(claims);
            result = LogInResult.Success(token, account.User!);
        }
        else
        {
            result = LogInResult.Fail(FailedLogIn);
        }

        return result;
    }

    private static IReadOnlyList<Claim> GetUserClaims(Account account)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, account.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, account.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, account.UserName!)
        };
        return claims;
    }
}