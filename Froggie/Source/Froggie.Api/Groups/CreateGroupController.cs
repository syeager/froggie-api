using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Data;
using LittleByte.EntityFramework;

namespace Froggie.Api.Groups;

public sealed class CreateGroupController(
    ICreateGroupService groupService,
    IMapper mapper,
    ISaveContextCommand contextCommand,
    IFindByIdQuery<User> userQuery)
    : GroupController(mapper)
{
    [HttpPost(Routes.Create)]
    [ResponseType(HttpStatusCode.Created, typeof(GroupDto))]
    [ResponseType(HttpStatusCode.BadRequest)]
    public async ValueTask<ApiResponse<GroupDto>> Create(CreateGroupRequest request)
    {
        var creator = await userQuery.FindRequiredAsync(request.CreatorId);

        var group = await groupService.CreateAsync(creator, request.Name);
        await contextCommand.CommitChangesAsync();

        var groupDto = mapper.Map<GroupDto>(group);
        return new CreatedResponse<GroupDto>(groupDto);
    }
}