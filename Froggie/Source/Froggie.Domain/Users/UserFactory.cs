namespace Froggie.Domain.Users;

public interface IUserFactory
{
    User Create(Id<User> id, string emailValue, string nameValue);
}

internal sealed class UserFactory : IUserFactory
{
    private readonly ModelValidator<User> userValidator;

    public UserFactory(ModelValidator<User> userValidator)
    {
        this.userValidator = userValidator;
    }

    public User Create(Id<User> id, string emailValue, string nameValue)
    {
        var email = new Email(emailValue);
        var name = new Name(nameValue);
        var user = User.Create(userValidator, id, email, name);

        return user;
    }
}