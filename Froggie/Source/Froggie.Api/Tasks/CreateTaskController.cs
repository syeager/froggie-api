using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.EntityFramework;

namespace Froggie.Api.Tasks;

public sealed class CreateTaskController(ICreateTaskService createTask, ISaveContextCommand context, IMapper mapper)
    : TaskController
{
    [HttpPost(Routes.Create)]
    [ResponseType(HttpStatusCode.Created, typeof(TaskDto))]
    [ResponseType(HttpStatusCode.BadRequest)]
    public async ValueTask<ApiResponse<TaskDto>> Create(CreateTaskRequest request)
    {
        var userId = new Id<User>(request.CreatorId);
        var groupId = new Id<Group>(request.GroupId);

        var result = await createTask.CreateAsync(request.Title, userId, request.DueDate, groupId);

        if(result.Succeeded)
        {
            await context.CommitChangesAsync();

            var dto = mapper.Map<TaskDto>(result.Value);
            return new CreatedResponse<TaskDto>(dto);
        }

        if(result is UserNeedsToBeInGroupToCreateTask)
        {
            throw new BadRequestException(result.ErrorMessage!);
        }

        throw new UnhandledInternalException();
    }
}