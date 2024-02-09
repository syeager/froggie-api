namespace Froggie.Domain.Users;

public sealed class User : DomainModel<User>
{
    public UserName Name { get; }

    private User(Id<User> id, UserName name)
        : base(id)
    {
        Name = name;
    }

    public static User Create(Id<User> id, UserName name)
    {
        var user = new User(id, name);
        var validator = new UserValidator();
        validator.SignOrThrow(user);

        return user;
    }
}