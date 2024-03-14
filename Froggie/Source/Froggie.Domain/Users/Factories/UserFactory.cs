namespace Froggie.Domain.Users;

public interface IUserFactory
{
    User Create(Id<User> id, UserName name);
}

internal sealed class UserFactory : IUserFactory
{
    public User Create(Id<User> id, UserName name) => User.Create(id, name);
}