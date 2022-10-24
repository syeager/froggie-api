using Froggie.Domain.Users.Models;

namespace Froggie.Domain.Users.Queries;

public interface IFindUserByEmailAndPasswordQuery
{
    ValueTask<User?> TryFindAsync(Email email, Password password);
}
