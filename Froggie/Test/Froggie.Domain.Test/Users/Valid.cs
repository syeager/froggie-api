using Froggie.Domain.Users;
using LittleByte.Common.Domain;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Users
    {
        public static readonly Email Email = new("user@example.com");
        public static readonly Name Name = new(new string('a', 1));
        public static readonly Password Password = new(new string('a', 1));

        public static User New()
        {
            return User.Create(new Id<User>(), Email, Name);
        }
    }
}