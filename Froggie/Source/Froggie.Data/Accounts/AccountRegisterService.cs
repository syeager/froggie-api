using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common.Logging;
using LittleByte.Domain;

namespace Froggie.Data.Accounts;

public interface IAccountRegisterService
{
    ValueTask<Result<User>> RegisterAsync(string emailValue, string nameValue, string passwordValue);
}

public sealed class UsernameIsNotAvailable() : Result<User>(false, "Username is not available");
public sealed class EmailAlreadyExists() : Result<User>(false, "An account with this email already exists");

internal sealed class AccountRegisterService(
    IAddUserCommand userCommand,
    IFindAccountByEmailQuery accountByEmailQuery,
    IDoesUserWithNameExistQuery userWithNameExistQuery,
    ICreateGroupService groupService,
    ICreateAccountCommand createAccountCommand)
    : IAccountRegisterService
{
    public async ValueTask<Result<User>> RegisterAsync(string emailValue, string nameValue, string passwordValue)
    {
        using var logger = this.NewLogger()
            .Push("User.Email", emailValue)
            .Push("User.Name", nameValue)
            .Info("Register user");

        var nameIsTaken = await userWithNameExistQuery.SearchAsync(nameValue);
        if(nameIsTaken)
        {
            return new UsernameIsNotAvailable();
        }

        var email = new Email(emailValue);
        var account = await accountByEmailQuery.FindAsync(email);
        if(account is not null)
        {
            return new EmailAlreadyExists();
        }

        var id = new Id<User>();
        var name = new UserName(nameValue);
        var user = User.Create(id, name);
        userCommand.Add(user);

        var password = new Password(passwordValue);
        await createAccountCommand.CreateAsync(user, email, password);

        // TODO: Add to a new RegisterResult.
        groupService.CreatePersonal(user);

        return Result<User>.Success(user);
    }
}