using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Tasks;

public sealed class CompleteTaskRequest
{
    [Required]
    public Guid TaskId { get; init; }
}

public sealed class CompleteTaskController : TaskController
{
    public ValueTask<ApiResponse<TaskDto>> Complete(CompleteTaskRequest request)
    {
        throw new NotImplementedException();
    }
}