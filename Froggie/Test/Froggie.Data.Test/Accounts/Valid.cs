using Froggie.Data.Accounts;

// ReSharper disable once CheckNamespace
namespace Froggie.Data.Test;

public static partial class Valid
{
    public static class Accounts
    {
        public static readonly Email Email = new("user@example.com");
        public static readonly Email Email2 = new("user2@example.com");
        public static readonly Password Password = new(new string('a', 1));

        public static Account New() => new()
        {
            UserName = Domain.Test.Valid.Users.Name,
            Email = Email,
        };
    }
}