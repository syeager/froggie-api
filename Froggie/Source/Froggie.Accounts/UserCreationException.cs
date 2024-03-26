namespace Froggie.Accounts;

public sealed class UserCreationException(IEnumerable<UserCreationException.Error> errors) : Exception
{
    public IReadOnlyList<Error> Errors { get; } = new List<Error>(errors);

    public override string ToString() => string.Join("\n- ", Errors.Select(e => $"{e.Code}: {e.Description}"));

    public readonly record struct Error(string Code, string Description);
}