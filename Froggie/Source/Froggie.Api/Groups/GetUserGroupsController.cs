using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.AutoMapper;

namespace Froggie.Api.Groups;

public sealed class GetUserGroupsController : GroupController
{
    private readonly IGetUserGroupsQuery getGroupsQuery;

    public GetUserGroupsController(IGetUserGroupsQuery getGroupsQuery, IMapper mapper)
        : base(mapper)
    {
        this.getGroupsQuery = getGroupsQuery;
    }

    [HttpGet("user-groups")]
    [ResponseType(HttpStatusCode.OK, typeof(GroupDto[]))]
    public async ValueTask<ApiResponse<GroupDto[]>> GetGroups(Guid id)
    {
        var userId = new Id<User>(id);
        var groups = await getGroupsQuery.QueryAsync(userId);
        var dtos = mapper.MapRange<GroupDto>(groups);
        return new OkResponse<GroupDto[]>(dtos);
    }
}