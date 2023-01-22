using Froggie.Domain.Users;
using LittleByte.Common.Domain;
using LittleByte.Test.Validation;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Users
    {
        public static readonly Email Email = new("user@example.com");
        public static readonly Email Email2 = new("user2@example.com");
        public static readonly Name Name = new(new string('a', 1));
        public static readonly Name Name2 = new(new string('b', 1));
        public static readonly Password Password = new(new string('a', 1));

        public static User New()
        {
            return User.Create(Validator.WillPass<User>(), new Id<User>(), Email, Name);
        }
    }
}