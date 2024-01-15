using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

internal sealed class UserConverter(UserManager<UserDao> userManager, IUserFactory factory)
    : ITypeConverter<User, UserDao>, ITypeConverter<UserDao, User>
{
    public UserDao Convert(User source, UserDao destination, ResolutionContext context)
    {
        var dao = new UserDao
        {
            Id = source.Id,
            Email = source.Email,
            UserName = source.Name
        };

        dao.NormalizedEmail = userManager.NormalizeEmail(dao.Email);
        dao.NormalizedUserName = userManager.NormalizeName(dao.UserName);

        return dao;
    }

    public User Convert(UserDao source, User destination, ResolutionContext context)
    {
        var userId = new Id<User>(source.Id);
        var user = factory.Create(userId, source.Email!, source.UserName!);
        return user;
    }
}