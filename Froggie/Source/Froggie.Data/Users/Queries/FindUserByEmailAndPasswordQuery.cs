using AutoMapper;
using Froggie.Data.Users.Models;
using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Froggie.Data.Users.Queries;

internal sealed class FindUserByEmailAndPasswordQuery : IFindUserByEmailAndPasswordQuery
{
    private readonly IMapper mapper;
    private readonly UserManager<UserDao> userManager;

    public FindUserByEmailAndPasswordQuery(UserManager<UserDao> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async ValueTask<User?> TryFindAsync(Email email, Password password)
    {
        var userEntity = await userManager.FindByEmailAsync(email.Value);
        if(userEntity is null)
        {
            return null;
        }

        Log.Information("Found user by email {Email} with Id {Id}", email.Value, userEntity.Id);
        var correctPassword = await userManager.CheckPasswordAsync(userEntity, password.Value);

        if(!correctPassword)
        {
            return null;
        }

        var user = mapper.Map<User>(userEntity);
        return user;
    }
}