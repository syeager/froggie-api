using Froggie.Domain.Users.Commands;
using Froggie.Domain.Users.Models;

namespace Froggie.Domain.Users.Services;

public interface IUserRegisterService
{
    Task<User> RegisterAsync(Email email, Name name, Password password);
}

public sealed class UserRegisterService : IUserRegisterService
{
    private readonly IAddUserCommand addUserCommand;

    public UserRegisterService(IAddUserCommand addUserCommand)
    {
        this.addUserCommand = addUserCommand;
    }

    // TODO: Confirm password.
    // TODO: Use password.
    // TODO: Check for existing user with email or name.
    public async Task<User> RegisterAsync(Email email, Name name, Password password)
    {
        var id = Guid.NewGuid();
        var user = User.Create(id, email, name);

        await addUserCommand.AddAsync(user, password);

        return user;
    }
}
