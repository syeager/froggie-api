using Froggie.Data.Users;
using LittleByte.Data;

namespace Froggie.Api.Users;

public sealed class GetUserPageController(IMapper mapper, IUserPageQuery pageQuery) : UserController
{
    [HttpGet(Routes.GetByPage)]
    [ResponseType(HttpStatusCode.OK, typeof(Page<UserDto>))]
    public async ValueTask<ApiResponse<Page<UserDto>>> GetPage([FromQuery] PageRequest? request)
    {
        request ??= new PageRequest();
        var response = await pageQuery.RunAsync(request);
        var dtos = response.CastResults(mapper.Map<UserDto>);
        return new OkResponse<Page<UserDto>>(dtos);
    }
}