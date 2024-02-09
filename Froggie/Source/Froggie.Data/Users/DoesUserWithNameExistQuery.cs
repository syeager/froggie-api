using Froggie.Data.Accounts;
using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

internal sealed class DoesUserWithNameExistQuery(FroggieDb database, UserManager<Account> accountManager)
    : IDoesUserWithNameExistQuery
{
    public async ValueTask<bool> SearchAsync(string nameValue)
    {
        var normalizedName = accountManager.NormalizeName(nameValue);
        var exists = await database.Accounts
            .AnyAsync(user => user.NormalizedUserName == normalizedName).NoAwait();

        return exists;
    }
}