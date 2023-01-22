namespace Froggie.Domain.Users;

public interface IFindUserByEmailQuery
{
    ValueTask<User?> FindAsync(string emailValue);
}