namespace Froggie.Domain.Users;

public interface IAddUserCommand
{
    void Add(User user);
}