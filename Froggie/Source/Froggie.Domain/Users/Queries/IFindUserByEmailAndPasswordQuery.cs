namespace Froggie.Domain.Users;

public interface IFindUserByEmailAndPasswordQuery
{
    ValueTask<User?> TryFindAsync(Email email, Password password);
}
