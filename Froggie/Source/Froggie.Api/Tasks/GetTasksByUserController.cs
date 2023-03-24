using Froggie.Domain.Tasks;
using Froggie.Domain.Users;

namespace Froggie.Api.Tasks;

public sealed class GetTasksByUserController : TaskController
{
    private readonly IMapper mapper;
    private readonly IGetUsersTasksService usersTasksService;

    public GetTasksByUserController(IGetUsersTasksService usersTasksService, IMapper mapper)
    {
        this.usersTasksService = usersTasksService;
        this.mapper = mapper;
    }

    [HttpGet("get-user-tasks")]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<TaskDto>))]
    public async ValueTask<ApiResponse<PageResponse<TaskDto>>> GetTasksByUser(GetTasksByUserRequest request)
    {
        var userId = new Id<User>(request.UserId);
        var tasks = await usersTasksService.FindAsync(userId);

        var response = tasks.CastResults<TaskDto>(mapper);
        return new OkResponse<PageResponse<TaskDto>>(response);
    }
}