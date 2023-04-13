using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common.AspNet.AutoMapper;

namespace Froggie.Api.Groups;

public sealed class GetUserGroupsController : GroupController
{
    private readonly IGetUsersGroupsQuery getGroupsQuery;

    public GetUserGroupsController(IGetUsersGroupsQuery getGroupsQuery, IMapper mapper)
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