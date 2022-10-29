using Froggie.Domain.Users.Models;

namespace Froggie.Domain.Users.Factories;

public interface IUserFactory
{
    User Create(Guid idValue, string emailValue, string nameValue);
}

internal sealed class UserFactory : IUserFactory
{
    public User Create(Guid idValue, string emailValue, string nameValue)
    {
        var email = new Email(emailValue);
        var name = new Name(nameValue);
        var user = User.Create(idValue, email, name);

        return user;
    }
}