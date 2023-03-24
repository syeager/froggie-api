using Froggie.Domain.Groups;
using LittleByte.Common.Infra.Commands;

namespace Froggie.Api.Groups;

public sealed class CreateGroupController : GroupController
{
    private readonly ICreateGroupService createGroupService;
    private readonly ISaveContextCommand saveContextCommand;

    public CreateGroupController(ICreateGroupService createGroupService,
                                 IMapper mapper,
                                 ISaveContextCommand saveContextCommand)
        : base(mapper)
    {
        this.createGroupService = createGroupService;
        this.saveContextCommand = saveContextCommand;
    }

    [HttpPost]
    public async ValueTask<ApiResponse<GroupDto>> Create(CreateGroupRequest request)
    {
        var group = await createGroupService.CreateAsync(request.Name);
        await saveContextCommand.CommitChangesAsync();

        var groupDto = mapper.Map<GroupDto>(group);
        return new CreatedResponse<GroupDto>(groupDto);
    }
}