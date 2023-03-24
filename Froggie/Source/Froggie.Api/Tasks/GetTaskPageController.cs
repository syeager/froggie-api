using Froggie.Data.Tasks;

namespace Froggie.Api.Tasks;

public sealed class GetTaskPageController : TaskController
{
    private readonly IMapper mapper;
    private readonly ITaskPageQuery taskPageQuery;

    public GetTaskPageController(ITaskPageQuery taskPageQuery, IMapper mapper)
    {
        this.taskPageQuery = taskPageQuery;
        this.mapper = mapper;
    }

    [HttpGet(Routes.GetByPage)]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<TaskDto>))]
    public async Task<ApiResponse<PageResponse<TaskDto>>> GetPage([FromQuery] PageRequest? request)
    {
        request ??= new PageRequest();
        var response = await taskPageQuery.RunAsync(request);
        var dtos = response.CastResults(mapper.Map<TaskDto>);
        return new OkResponse<PageResponse<TaskDto>>(dtos);
    }
}