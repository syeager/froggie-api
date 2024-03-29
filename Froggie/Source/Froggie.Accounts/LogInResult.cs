using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;

namespace Froggie.Accounts;

public sealed class LogInResult
{
    [MemberNotNullWhen(true, nameof(Succeeded))]
    public JwtSecurityToken? AccessToken { get; }
    [MemberNotNullWhen(true, nameof(Succeeded))]
    public Account? Account { get; }
    [MemberNotNullWhen(false, nameof(Succeeded))]
    public IReadOnlyList<string>? Errors { get; }

    public bool Succeeded { get; }

    private LogInResult(bool succeeded, JwtSecurityToken? accessToken, Account? account, IEnumerable<string>? errors)
    {
        Succeeded = succeeded;
        AccessToken = accessToken;
        Account = account;
        Errors = errors?.ToArray();
    }

    public static LogInResult Success(JwtSecurityToken accessToken, Account account) => new(true, accessToken, account, null);

    public static LogInResult Fail(IEnumerable<string> error) => new(false, null, null, error);
}