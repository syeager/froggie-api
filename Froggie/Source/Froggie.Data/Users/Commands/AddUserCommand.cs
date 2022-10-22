using AutoMapper;
using Froggie.Data.Users.Models;
using Froggie.Domain.Users.Commands;
using Froggie.Domain.Users.Models;
using LittleByte.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using User = Froggie.Domain.Users.Models.User;

namespace Froggie.Data.Users.Commands;

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
        var result = await userManager.CreateAsync(userDao, password.Value);

        if(!result.Succeeded)
        {
            throw new BadRequestException(result.ToString());
        }
    }
}