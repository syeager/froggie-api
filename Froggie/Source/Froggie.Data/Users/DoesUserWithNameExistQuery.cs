using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Data.Users;

internal sealed class DoesUserWithNameExistQuery : IDoesUserWithNameExistQuery
{
    private readonly FroggieDb database;
    private readonly UserManager<UserDao> userManager;

    public DoesUserWithNameExistQuery(FroggieDb database, UserManager<UserDao> userManager)
    {
        this.database = database;
        this.userManager = userManager;
    }

    public async ValueTask<bool> SearchAsync(string nameValue)
    {
        var normalizedName = userManager.NormalizeName(nameValue);
        var exists = await database.Users
            .AnyAsync(user => user.NormalizedUserName == normalizedName).ConfigureAwait(false);

        return exists;
    }
}