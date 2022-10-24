using AutoMapper;
using Froggie.Data.Users.Models;
using Froggie.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users.Mappings;

internal sealed class UserMap : ITypeConverter<User, UserDao>, ITypeConverter<UserDao, User>
{
    private readonly UserManager<UserDao> userManager;

    public UserMap(UserManager<UserDao> userManager)
    {
        this.userManager = userManager;
    }

    public UserDao Convert(User source, UserDao destination, ResolutionContext context)
    {
        var dao = new UserDao
        {
            Id = source.Id.Value,
            Email = source.Email.Value,
            UserName = source.Name.Value
        };

        dao.NormalizedEmail = userManager.NormalizeEmail(dao.Email);
        dao.NormalizedUserName = userManager.NormalizeName(dao.UserName);

        return dao;
    }

    public User Convert(UserDao source, User destination, ResolutionContext context)
    {
        var user = User.Create(source.Id, new Email(source.Email), new Name(source.UserName));
        return user;
    }
}