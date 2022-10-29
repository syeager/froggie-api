namespace Froggie.Domain.Users;

public interface IUserRegisterService
{
    ValueTask<User> RegisterAsync(string emailValue, string nameValue, string passwordValue);
}

internal sealed class UserRegisterService : IUserRegisterService
{
    private readonly IAddUserCommand addUserCommand;
    private readonly IUserFactory userFactory;

    public UserRegisterService(IAddUserCommand addUserCommand, IUserFactory userFactory)
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
