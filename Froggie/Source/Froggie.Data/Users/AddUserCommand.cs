using Froggie.Domain.Users;

namespace Froggie.Data.Users;

internal sealed class AddUserCommand(FroggieDb froggieDb) : IAddUserCommand
{
    public void Add(User user) => froggieDb.Users.Add(user);
}