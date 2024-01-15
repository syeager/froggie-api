using Froggie.Domain.Groups;

namespace Froggie.Domain.Users;

public interface IUserRegisterService
{
    ValueTask<User> RegisterAsync(string emailValue, string nameValue, string passwordValue);
}

internal sealed class UserRegisterService(
    IAddUserCommand userCommand,
    IUserFactory factory,
    IFindUserByEmailQuery userByEmailQuery,
    IDoesUserWithNameExistQuery userWithNameExistQuery,
    ICreateGroupService groupService)
    : IUserRegisterService
{
    public async ValueTask<User> RegisterAsync(string emailValue, string nameValue, string passwordValue)
    {
        using var logger = this.NewLogger()
            .Push("User.Email", emailValue)
            .Push("User.Name", nameValue)
            .Info("Register user");

        var nameIsTaken = await userWithNameExistQuery.SearchAsync(nameValue);
        if(nameIsTaken)
        {
            throw new NameIsTakenException(nameValue);
        }

        var user = await userByEmailQuery.FindAsync(emailValue);
        if(user is not null)
        {
            throw new EmailIsTakenException(emailValue);
        }

        var id = new Id<User>();
        user = factory.Create(id, emailValue, nameValue);

        var password = new Password(passwordValue);
        await userCommand.AddAsync(user, password);

        // TODO: Add to a new RegisterResult.
        _ = await groupService.CreatePersonalAsync(user);

        return user;
    }
}