using Froggie.Domain.Groups;

namespace Froggie.Api.Groups;

public sealed class GetGroupController : GroupController
{
    private readonly IFindByIdQuery<Group> getGroup;

    public GetGroupController(IFindByIdQuery<Group> getGroup, IMapper mapper)
        : base(mapper)
    {
        this.getGroup = getGroup;
    }

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