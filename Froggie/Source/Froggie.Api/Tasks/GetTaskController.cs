using LittleByte.Domain;

namespace Froggie.Api.Tasks;

public sealed class GetTaskController(IFindByIdQuery<Task> task, IMapper mapper) : TaskController
{
    [HttpGet(Routes.GetById)]
    [ResponseType(HttpStatusCode.OK, typeof(TaskDto))]
    [ResponseType(HttpStatusCode.NotFound)]
    public async ValueTask<ApiResponse<TaskDto>> GetTask(Guid id)
    {
        var validTask = await task.FindRequiredAsync(id);
        var taskDto = mapper.Map<TaskDto>(validTask);
        return new OkResponse<TaskDto>(taskDto);
    }
}