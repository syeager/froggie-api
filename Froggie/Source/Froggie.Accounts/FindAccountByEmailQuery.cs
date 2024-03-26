using LittleByte.Common;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Accounts;

public interface IFindAccountByEmailQuery
{
    ValueTask<Account?> FindAsync(Email email);
}

internal sealed class FindAccountByEmailQuery(AccountsDb accountsDb, IAccountManager accountManager) : IFindAccountByEmailQuery
{
    public async ValueTask<Account?> FindAsync(Email email)
    {
        var normalizedEmail = accountManager.NormalizeEmail(email);
        var account = await accountsDb.Users.FirstOrDefaultAsync(a => a.NormalizedEmail == normalizedEmail).NoAwait();

        return account;
    }
}