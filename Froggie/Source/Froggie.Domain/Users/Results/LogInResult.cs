using System.IdentityModel.Tokens.Jwt;

namespace Froggie.Domain.Users;

public class LogInResult
{
    [MemberNotNullWhen(true, nameof(Succeeded))]
    public JwtSecurityToken? AccessToken { get; }

    [MemberNotNullWhen(false, nameof(Succeeded))]
    public IReadOnlyList<string>? Errors { get; }

    public bool Succeeded { get; }

    [MemberNotNullWhen(true, nameof(Succeeded))]
    public User? User { get; }

    private LogInResult(bool succeeded, JwtSecurityToken? accessToken, User? user, IEnumerable<string>? errors)
    {
        Succeeded = succeeded;
        AccessToken = accessToken;
        User = user;
        Errors = errors?.ToArray();
    }

    public static LogInResult Success(JwtSecurityToken authToken, User user) => new(true, authToken, user, null);

    public static LogInResult Fail(IEnumerable<string> error) => new(false, null, null, error);
}