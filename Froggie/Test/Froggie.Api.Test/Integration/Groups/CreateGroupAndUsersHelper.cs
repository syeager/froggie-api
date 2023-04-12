using Froggie.Domain.Groups;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Groups;

public static class CreateGroupAndUsersHelper
{
    public static async ValueTask<Group> CreateAsync(IServiceProvider services,
                                                     string groupName = "test-group",
                                                     int userCount = 2)
    {
        var registerUser = services.GetRequiredService<IUserRegisterService>();
        var createGroup = services.GetRequiredService<ICreateGroupService>();
        var addUserToGroup = services.GetRequiredService<IAddUserToGroupService>();

        var users = userCount.Execute(i =>
        {
            var name = $"{groupName}{i}";
            return registerUser.RegisterAsync($"{name}@mail.com", name, Valid.Users.Password).Result;
        });

        var group = await createGroup.CreateAsync(users.First(), groupName);
        for(var i = 1; i < userCount; i++)
        {
            await addUserToGroup.AddAsync(users[i], group);
        }

        return group;
    }
}