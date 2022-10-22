using Froggie.Domain.Users.Models;

namespace Froggie.Domain.Users.Commands;

public interface IAddUserCommand
{
    ValueTask AddAsync(User user, Password password);
}
