using Froggie.Domain.Groups;
using LittleByte.Data;

namespace Froggie.Api.Groups;

public sealed class GetGroupController(IFindByIdQuery<Group> getGroup, IMapper mapper) : GroupController(mapper)
{
    [HttpGet(Routes.GetById)]
    [ResponseType(HttpStatusCode.OK, typeof(GroupDto))]
    [ResponseType(HttpStatusCode.NotFound)]
    public async ValueTask<ApiResponse<GroupDto>> GetGroup(Guid id)
    {
        var group = await getGroup.FindRequiredAsync(id);
        var dto = mapper.Map<GroupDto>(group);
        return new OkResponse<GroupDto>(dto);
    }
}