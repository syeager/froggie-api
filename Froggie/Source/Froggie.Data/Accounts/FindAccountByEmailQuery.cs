namespace Froggie.Data.Accounts;

public interface IFindAccountByEmailQuery
{
    ValueTask<Account?> FindAsync(Email email);
}

internal sealed class FindAccountByEmailQuery(FroggieDb froggieDb, IAccountManager accountManager) : IFindAccountByEmailQuery
{
    public async ValueTask<Account?> FindAsync(Email email)
    {
        var normalizedEmail = accountManager.NormalizeEmail(email);
        var account = await froggieDb.Accounts.FirstOrDefaultAsync(a => a.NormalizedEmail == normalizedEmail).NoAwait();

        return account;
    }
}