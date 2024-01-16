using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Domain;
using LittleByte.EntityFramework;

namespace Froggie.Api.Groups;

public sealed class AddUserToGroupController(
    IMapper mapper,
    IFindByIdQuery<User> findUserQuery,
    IFindByIdQuery<Group> findGroupQuery,
    IAddUserToGroupService addUserToGroupService,
    ISaveContextCommand saveCommand)
    : GroupController(mapper)
{
    [HttpPost("add-member")]
    [ResponseType(HttpStatusCode.OK)]
    public async ValueTask<ApiResponse> AddUser(AddUserToGroupRequest request)
    {
        var user = await findUserQuery.FindRequiredAsync(request.UserId);
        var group = await findGroupQuery.FindRequiredAsync(request.GroupId);

        await addUserToGroupService.AddAsync(user, group);

        await saveCommand.CommitChangesAsync();

        return new OkResponse();
    }
}