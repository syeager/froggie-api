using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.AutoMapper;

namespace Froggie.Api.Groups;

public sealed record GetUserGroupsRequest([Required] Guid UserId);

public sealed class GetUserGroupsController(IGetUserGroupsQuery getGroupsQuery, IMapper mapper)
    : GroupController
{
    // TODO: Return Page.
    [HttpGet("user-groups")]
    [ResponseType(HttpStatusCode.OK, typeof(GroupDto[]))]
    public async ValueTask<ApiResponse<GroupDto[]>> GetGroups([FromQuery] GetUserGroupsRequest request)
    {
        var userId = new Id<User>(request.UserId);
        var groups = await getGroupsQuery.QueryAsync(userId);
        var dtos = mapper.MapRange<GroupDto>(groups);
        return new OkResponse<GroupDto[]>(dtos);
    }
}