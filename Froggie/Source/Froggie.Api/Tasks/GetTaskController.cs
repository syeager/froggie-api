namespace Froggie.Api.Tasks;

public sealed class GetTaskController : TaskController
{
    private readonly IFindByIdQuery<Task> getTask;
    private readonly IMapper mapper;

    public GetTaskController(IFindByIdQuery<Task> getTask, IMapper mapper)
    {
        this.getTask = getTask;
        this.mapper = mapper;
    }

    [HttpGet(Routes.GetById)]
    [ResponseType(HttpStatusCode.OK, typeof(TaskDto))]
    public async ValueTask<ApiResponse<TaskDto>> GetTask(Guid id)
    {
        var validTask = await getTask.FindRequiredAsync(id);
        var taskDto = mapper.Map<TaskDto>(validTask);
        return new OkResponse<TaskDto>(taskDto);
    }
}