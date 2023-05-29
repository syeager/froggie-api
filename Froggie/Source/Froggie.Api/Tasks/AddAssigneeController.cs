namespace Froggie.Api.Tasks;

public sealed class AddAssigneeController : TaskController
{
    [HttpPost("assignee-add")]
    [ResponseType(HttpStatusCode.OK, typeof(TaskDto))]
    public ValueTask<ApiResponse<TaskDto>> AddAssignee()
    {
        throw new NotImplementedException();
    }
}