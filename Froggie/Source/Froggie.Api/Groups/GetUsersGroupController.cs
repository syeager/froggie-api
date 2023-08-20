using Froggie.Api.Users;
using Froggie.Domain.Groups;

namespace Froggie.Api.Groups;

public sealed class GetUsersGroupController : GroupController
{
    private readonly IGetUsersInGroupQuery usersInGroupQuery;

    public GetUsersGroupController(IGetUsersInGroupQuery usersInGroupQuery, IMapper mapper)
        : base(mapper)
    {
        this.usersInGroupQuery = usersInGroupQuery;
    }

    [HttpGet("users")]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<UserDto>))]
    public async ValueTask<ApiResponse<PageResponse<UserDto>>> GetUsers(Guid id)
    {
        var groupId = new Id<Group>(id);

        var users = await usersInGroupQuery.QueryAsync(groupId).NoWait();
        var dto = users.CastResults<UserDto>(mapper);

        return new OkResponse<PageResponse<UserDto>>(dto);
    }
}