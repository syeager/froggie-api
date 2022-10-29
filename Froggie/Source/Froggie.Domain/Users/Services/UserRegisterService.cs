using Froggie.Domain.Users.Commands;
using Froggie.Domain.Users.Factories;
using Froggie.Domain.Users.Models;

namespace Froggie.Domain.Users.Services;

public interface IUserRegisterService
{
    ValueTask<User> RegisterAsync(string emailValue, string nameValue, string passwordValue);
}

internal sealed class UserRegisterService : IUserRegisterService
{
    private readonly IAddUserCommand addUserCommand;
    private readonly IUserFactory userFactory;

    internal UserRegisterService(IAddUserCommand addUserCommand, IUserFactory userFactory)
    {
        this.addUserCommand = addUserCommand;
        this.userFactory = userFactory;
    }

    public async ValueTask<User> RegisterAsync(string emailValue, string nameValue, string passwordValue)
    {
        var id = Guid.NewGuid();
        var user = userFactory.Create(id, emailValue, nameValue);

        var password = new Password(passwordValue);
        await addUserCommand.AddAsync(user, password);

        return user;
    }
}
