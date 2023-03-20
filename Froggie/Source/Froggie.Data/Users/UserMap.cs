using AutoMapper;
using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

internal sealed class UserMap : ITypeConverter<User, UserDao>, ITypeConverter<UserDao, User>
{
    private readonly IUserFactory userFactory;
    private readonly UserManager<UserDao> userManager;

    public UserMap(UserManager<UserDao> userManager, IUserFactory userFactory)
    {
        this.userManager = userManager;
        this.userFactory = userFactory;
    }

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
        var user = userFactory.Create(source.Id, source.Email!, source.UserName!);
        return user;
    }
}