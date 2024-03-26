using Froggie.Accounts;

namespace Froggie.Test;

public static class ValidAccount
{
    public static readonly Email Email = new("user@example.com");
    public static readonly Email Email2 = new("user2@example.com");
    public static readonly Password Password = new(new string('a', 1));

    public static Account New() => new()
    {
        UserName = ValidUser.Name,
        Email = Email
    };
}