using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Domain;
using LittleByte.EntityFramework;

namespace Froggie.Api.Groups;

public sealed class CreateGroupController(
    ICreateGroupService groupService,
    IMapper mapper,
    ISaveContextCommand contextCommand,
    IFindByIdQuery<User> userQuery)
    : GroupController
{
    [HttpPost(Routes.Create)]
    [ResponseType(HttpStatusCode.Created, typeof(GroupDto))]
    [ResponseType(HttpStatusCode.BadRequest)]
    public async ValueTask<ApiResponse<GroupDto>> Create(CreateGroupRequest request)
    {
        var creator = await userQuery.FindRequiredAsync(request.CreatorId.ToId<User>());

        var name = new GroupName(request.Name);
        var group = groupService.Create(creator, name);
        await contextCommand.CommitChangesAsync();

        var groupDto = mapper.Map<GroupDto>(group);
        return new CreatedResponse<GroupDto>(groupDto);
    }
}