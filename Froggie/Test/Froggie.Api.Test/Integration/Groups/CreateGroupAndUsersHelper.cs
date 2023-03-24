using Froggie.Domain.Groups;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
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

        var group = await createGroup.CreateAsync(groupName);

        for(var i = 0; i < userCount; i++)
        {
            var name = $"{group.Name.Value}{i}";
            var user = await registerUser.RegisterAsync($"{name}@mail.com", name, Valid.Users.Password);
            await addUserToGroup.AddAsync(user, group);
        }

        return group;
    }
}