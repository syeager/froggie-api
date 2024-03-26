using LittleByte.Common;
using LittleByte.Common.Logging;

namespace Froggie.Accounts;

public interface IFindAccountByEmailAndPassword
{
    ValueTask<Account?> TryFindAsync(Email email, Password password);
}

internal class FindAccountByEmailAndPassword(IAccountManager accountManager) : IFindAccountByEmailAndPassword
{
    public async ValueTask<Account?> TryFindAsync(Email email, Password password)
    {
        using var logger = this.NewLogger().Push("User.Email", email.ToString());

        var account = await accountManager.FindByEmailAsync(email).NoAwait();

        if(account is null)
        {
            logger.Info("No account with email found");
            return null;
        }

        logger.Debug("Found user with email");

        var correctPassword = await accountManager
            .CheckPasswordAsync(account, password)
            .NoAwait();

        if(!correctPassword)
        {
            logger.Info("Password check failed");
            return null;
        }

        return account;
    }
}