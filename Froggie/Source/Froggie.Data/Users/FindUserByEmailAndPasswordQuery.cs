using Froggie.Domain.Users;
using LittleByte.Common.Logging;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

internal sealed class FindUserByEmailAndPasswordQuery(UserManager<UserDao> userManager, IMapper mapper)
    : IFindUserByEmailAndPasswordQuery
{
    public async ValueTask<User?> TryFindAsync(Email email, Password password)
    {
        using var logger = this.NewLogger().Push("User.Email", email.ToString());

        var userEntity = await userManager
            .FindByEmailAsync(email)
            .ConfigureAwait(false);

        if(userEntity is null)
        {
            logger.Info("No user with email found");
            return null;
        }

        logger.Info("Found user with email");

        var correctPassword = await userManager
            .CheckPasswordAsync(userEntity, password)
            .ConfigureAwait(false);

        if(!correctPassword)
        {
            logger.Info("Password check failed");
            return null;
        }

        var user = mapper.Map<User>(userEntity);
        return user;
    }
}