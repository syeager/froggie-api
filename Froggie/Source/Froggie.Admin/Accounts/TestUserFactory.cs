using Froggie.Domain.Users;
using LittleByte.Common;

namespace Froggie.Admin.Accounts;

public sealed class TestUserFactory : IUserFactory
{
    public User Create(Id<User> _, UserName name) => User.Create(UserReserve.TestUser.ToId<User>(), name);
}