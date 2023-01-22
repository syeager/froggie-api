namespace Froggie.Domain.Users;

public interface IAddUserCommand
{
    ValueTask AddAsync(User user, Password password);
}