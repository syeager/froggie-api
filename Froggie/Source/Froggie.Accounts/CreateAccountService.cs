using Froggie.Domain.Users;
using LittleByte.Common.Logging;
using LittleByte.Domain;

namespace Froggie.Accounts;

public interface ICreateAccountService
{
    ValueTask<Result<Account>> CreateAsync(string emailValue, string nameValue, string passwordValue, Guid? idValue = null);
}

public sealed class UsernameIsNotAvailable() : Result<Account>(false, "Username is not available");

public sealed class EmailAlreadyExists() : Result<Account>(false, "An account with this email already exists");

public class CreateAccountService(
    IFindAccountByEmailQuery accountByEmailQuery,
    IDoesUserWithNameExistQuery userWithNameExistQuery,
    ICreateAccountCommand createAccountCommand)
    : ICreateAccountService
{
    public async ValueTask<Result<Account>> CreateAsync(string emailValue, string nameValue, string passwordValue, Guid? idValue = null)
    {
        using var logger = this.NewLogger()
            .Push("User.Email", emailValue)
            .Push("User.Name", nameValue)
            .Info("Register user");

        var name = new UserName(nameValue);
        var nameIsTaken = await userWithNameExistQuery.SearchAsync(nameValue);
        if(nameIsTaken)
        {
            return new UsernameIsNotAvailable();
        }

        var email = new Email(emailValue);
        var existingAccount = await accountByEmailQuery.FindAsync(email);
        if(existingAccount is not null)
        {
            return new EmailAlreadyExists();
        }

        var id = idValue ?? Guid.NewGuid();
        var password = new Password(passwordValue);
        var account = await createAccountCommand.CreateAsync(id, name, email, password);

        return Result<Account>.Success(account);
    }
}
