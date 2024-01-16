using Froggie.Api.Users;
using Froggie.Data.Groups;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Data;
using LittleByte.AutoMapper.Data;

namespace Froggie.Api.Groups;

public sealed class GetUsersGroupController(IGetUsersInGroupQuery inGroupQuery, IMapper mapper)
    : GroupController(mapper)
{
    [HttpGet("users")]
    [ResponseType(HttpStatusCode.OK, typeof(Page<UserDto>))]
    public async ValueTask<ApiResponse<Page<UserDto>>> GetUsers(Guid id)
    {
        var groupId = new Id<Group>(id);

        var users = await inGroupQuery.QueryAsync(groupId).NoAwait();
        var dto = users.CastResults<User, UserDto>(mapper);

        return new OkResponse<Page<UserDto>>(dto);
    }
}