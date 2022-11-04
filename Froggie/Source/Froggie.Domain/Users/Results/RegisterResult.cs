namespace Froggie.Domain.Users;

public class RegisterResult
{
    [MemberNotNullWhen(false, nameof(Succeeded))]
    public IReadOnlyList<string>? Errors { get; }

    public bool Succeeded { get; }

    [MemberNotNullWhen(true, nameof(Succeeded))]
    public Valid<User>? User { get; }

    private RegisterResult(bool succeeded, Valid<User>? user, IEnumerable<string>? errors)
    {
        Succeeded = succeeded;
        User = user;
        Errors = errors?.ToArray();
    }

    public static RegisterResult Success(Valid<User> user) => new(true, user, null);

    public static RegisterResult Fail(IEnumerable<string> errors) => new(false, null, errors);
}