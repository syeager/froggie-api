using Froggie.Domain.Users.Models;

namespace Froggie.Domain.Users.Queries;

public interface IFindUserByEmailAndPasswordQuery
{
    ValueTask<Valid<User>?> TryFindAsync(Email email, Password password);
}
