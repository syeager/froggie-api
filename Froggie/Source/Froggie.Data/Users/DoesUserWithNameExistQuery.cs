using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

internal sealed class DoesUserWithNameExistQuery(FroggieDb database, UserManager<UserDao> userManager)
    : IDoesUserWithNameExistQuery
{
    private readonly UserManager<UserDao> userManager = userManager;

    public async ValueTask<bool> SearchAsync(string nameValue)
    {
        var normalizedName = userManager.NormalizeName(nameValue);
        var exists = await database.Users
            .AnyAsync(user => user.NormalizedUserName == normalizedName).ConfigureAwait(false);

        return exists;
    }
}