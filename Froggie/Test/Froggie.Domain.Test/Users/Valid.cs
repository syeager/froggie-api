using Froggie.Domain.Users.Models;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class User
    {
        public static readonly Email Email = new("user@example.com");
        public static readonly Name Name = new(new string('a', 1));
        public static readonly Password Password = new(new string('a', 1));
    }
}