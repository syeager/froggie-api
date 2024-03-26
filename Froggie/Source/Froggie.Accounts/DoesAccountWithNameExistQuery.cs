using Froggie.Domain.Users;
using LittleByte.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Accounts;

internal sealed class DoesAccountWithNameExistQuery(AccountsDb accountsDb, UserManager<Account> accountManager)
    : IDoesUserWithNameExistQuery
{
    public async ValueTask<bool> SearchAsync(string nameValue)
    {
        var normalizedName = accountManager.NormalizeName(nameValue);
        var exists = await accountsDb.Users
            .AnyAsync(user => user.NormalizedUserName == normalizedName).NoAwait();

        return exists;
    }
}