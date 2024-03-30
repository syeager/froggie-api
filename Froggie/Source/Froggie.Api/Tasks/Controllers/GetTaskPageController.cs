using Froggie.Data.Tasks;
using LittleByte.Data;

namespace Froggie.Api.Tasks;

public sealed class GetTaskPageController(ITaskPageQuery pageQuery, IMapper mapper) : TaskController
{
    [HttpGet(Routes.GetByPage)]
    [ResponseType(HttpStatusCode.OK, typeof(Page<TaskDto>))]
    public async Task<ApiResponse<Page<TaskDto>>> GetPage([FromQuery] PageRequest? request)
    {
        request ??= new PageRequest();
        var response = await pageQuery.RunAsync(request);
        var dtos = response.CastResults(mapper.Map<TaskDto>);
        return new OkResponse<Page<TaskDto>>(dtos);
    }
}