using AutoMapper;
using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

internal sealed class FindUserByEmailQuery : IFindUserByEmailQuery
{
    private readonly UserManager<UserDao> userManager;
    private readonly IMapper mapper;

    public FindUserByEmailQuery(UserManager<UserDao> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async ValueTask<User?> FindAsync(string emailValue)
    {
        var userDao = await userManager.FindByEmailAsync(emailValue).ConfigureAwait(false);

        if(userDao is null)
        {
            return null;
        }

        var user = mapper.Map<User>(userDao);
        return user;
    }
}