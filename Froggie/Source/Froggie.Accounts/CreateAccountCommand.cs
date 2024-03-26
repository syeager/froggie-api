using Froggie.Domain.Users;

namespace Froggie.Accounts;

public interface ICreateAccountCommand
{
    ValueTask<Account> CreateAsync(Guid id, UserName userName, Email email, Password password);
}

internal sealed class CreateAccountCommand(IAccountManager accountManager) : ICreateAccountCommand
{
    public async ValueTask<Account> CreateAsync(Guid id, UserName userName, Email email, Password password)
    {
        var account = new Account
        {
            Id = id,
            UserName = userName,
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