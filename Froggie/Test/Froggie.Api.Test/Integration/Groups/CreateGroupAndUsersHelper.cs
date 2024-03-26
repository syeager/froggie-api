using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Groups;

public static class CreateGroupAndUsersHelper
{
    public static Group Create(IServiceProvider services,
                               string groupName = "test-group",
                               int userCount = 2)
    {
        var createGroup = services.GetRequiredService<ICreateGroupService>();

        var users = userCount.Execute(i =>
        {
            var name = new UserName($"{groupName}{i}");
            return User.Create(new Id<User>(), name);
        });

        var group = createGroup.Create(users.First(), new GroupName(groupName));
        for(var i = 1; i < userCount; i++)
        {
            group.AddUser(users[i]);
        }

        return group;
    }
}