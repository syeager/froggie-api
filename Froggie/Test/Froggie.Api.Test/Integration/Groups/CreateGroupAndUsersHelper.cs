using Froggie.Data.Accounts;
using Froggie.Data.Test;
using Froggie.Domain.Groups;
using LittleByte.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Groups;

public static class CreateGroupAndUsersHelper
{
    public static Group Create(IServiceProvider services,
                               string groupName = "test-group",
                               int userCount = 2)
    {
        var registerUser = services.GetRequiredService<IAccountRegisterService>();
        var createGroup = services.GetRequiredService<ICreateGroupService>();

        var users = userCount.Execute(i =>
        {
            var name = new GroupName($"{groupName}{i}");
            return registerUser.RegisterAsync($"{name}@mail.com", name, Valid.Accounts.Password).Result.Value!;
        });

        var group = createGroup.Create(users.First(), new GroupName(groupName));
        for(var i = 1; i < userCount; i++)
        {
            group.AddUser(users[i]);
        }

        return group;
    }
}