using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public interface IUserGroupCreateCommand
{
    public void Create(User user, Group group);
}