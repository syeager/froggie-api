using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;

namespace Froggie.Accounts;

public sealed class LogInResult
{
    [MemberNotNullWhen(true, nameof(Succeeded))]
    public JwtSecurityToken? AccessToken { get; }

    [MemberNotNullWhen(false, nameof(Succeeded))]
    public IReadOnlyList<string>? Errors { get; }

    public bool Succeeded { get; }

    private LogInResult(bool succeeded, JwtSecurityToken? accessToken, IEnumerable<string>? errors)
    {
        Succeeded = succeeded;
        AccessToken = accessToken;
        Errors = errors?.ToArray();
    }

    public static LogInResult Success(JwtSecurityToken accessToken) => new(true, accessToken, null);

    public static LogInResult Fail(IEnumerable<string> error) => new(false, null, error);
}