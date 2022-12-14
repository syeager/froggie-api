using AutoMapper;
using Froggie.Domain.Users;
using LittleByte.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

internal sealed class AddUserCommand : IAddUserCommand
{
    private readonly IMapper mapper;
    private readonly UserManager<UserDao> userManager;

    public AddUserCommand(UserManager<UserDao> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async ValueTask AddAsync(User user, Password password)
    {
        var userDao = mapper.Map<UserDao>(user);
        var result = await userManager.CreateAsync(userDao, password);

        if(!result.Succeeded)
        {
            throw new BadRequestException(result.ToString());
        }
    }
}