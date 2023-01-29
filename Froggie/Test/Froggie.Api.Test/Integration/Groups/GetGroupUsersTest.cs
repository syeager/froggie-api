using Froggie.Api.Groups;
using Froggie.Domain.Groups;
using Froggie.Domain.Test;
using Froggie.Domain.Users;
using LittleByte.Test.AspNet;

namespace Froggie.Api.Test.Integration.Groups;

public sealed class GetGroupUsersTest : ApiIntegrationTest<GetUsersGroupController>
{
    [Test]
    public async ValueTask QueryAllUsers()
    {
        const int userCount = 3;
        var group = await CreateGroupAndUsers("group", userCount);
        _ = await CreateGroupAndUsers("groupother", 2);
        await saveCommand.CommitChangesAsync();

        var users = await controller.GetUsers(group);

        ApiAssert.IsSuccess(users);
        Assert.AreEqual(userCount, users.Obj!.TotalResults);
    }

    #region Helpers

    private async ValueTask<Group> CreateGroupAndUsers(string groupName, int userCount)
    {
        var registerUser = GetService<IUserRegisterService>();
        var createGroup = GetService<ICreateGroupService>();
        var addUserToGroup = GetService<IAddUserToGroupService>();

        var group = await createGroup.CreateAsync(groupName);

        for(var i = 0; i < userCount; i++)
        {
            var name = $"{group.Name.Value}{i}";
            var user = await registerUser.RegisterAsync($"{name}@mail.com", name, Valid.Users.Password);
            await addUserToGroup.AddAsync(user, group);
        }

        return group;
    }

    #endregion
}