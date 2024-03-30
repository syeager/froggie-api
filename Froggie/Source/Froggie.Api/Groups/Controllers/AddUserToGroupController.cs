using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Domain;
using LittleByte.EntityFramework;

namespace Froggie.Api.Groups;

public sealed record AddUserToGroupRequest([Required] Guid UserId, [Required] Guid GroupId);

public sealed class AddUserToGroupController(
    IFindByIdQuery<User> findUserQuery,
    IFindByIdQuery<Group> findGroupQuery,
    ISaveContextCommand saveCommand)
    : GroupController
{
    [HttpPut("add-member")]
    [ResponseType(HttpStatusCode.OK)]
    public async ValueTask<ApiResponse> AddUser(AddUserToGroupRequest request)
    {
        var user = await findUserQuery.FindRequiredAsync(request.UserId.ToId<User>());
        var group = await findGroupQuery.FindRequiredAsync(request.GroupId.ToId<Group>());

        group.AddUser(user);

        await saveCommand.CommitChangesAsync();

        return new OkResponse();
    }
}