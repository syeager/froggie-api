namespace Froggie.Domain.Users;

public sealed class User : DomainModel<User>
{
    public Email Email { get; }
    public Name Name { get; }

    private User(Id<User> id, Email email, Name name)
        : base(id)
    {
        Email = email;
        Name = name;
    }

    internal static User Create(Id<User> id, Email email, Name name)
    {
        var user = new User(id, email, name);
        var validator = new UserValidator();
        validator.SignOrThrow(user);
        return user;
    }
}