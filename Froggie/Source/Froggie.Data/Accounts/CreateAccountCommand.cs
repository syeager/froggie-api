using Froggie.Data.Users;
using Froggie.Domain.Users;

namespace Froggie.Data.Accounts;

public interface ICreateAccountCommand
{
    ValueTask<Account> CreateAsync(User user, Email email, Password password);
}

internal sealed class CreateAccountCommand(IAccountManager accountManager) : ICreateAccountCommand
{
    public async ValueTask<Account> CreateAsync(User user, Email email, Password password)
    {
        var account = new Account
        {
            User = user,
            UserName = user.Name,
            Email = email,
        };

        var result = await accountManager.AddAsync(account, password);

        if(!result.Succeeded)
        {
            throw new UserCreationException(result.Errors.Select(e =>
                new UserCreationException.Error(e.Code, e.Description)));
        }

        return account;
    }
}