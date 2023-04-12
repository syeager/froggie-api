using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common.Infra.Commands;

namespace Froggie.Api.Groups;

public sealed class CreateGroupController : GroupController
{
    private readonly ICreateGroupService createGroupService;
    private readonly ISaveContextCommand saveContextCommand;
    private readonly IFindByIdQuery<User> findUserQuery;

    public CreateGroupController(ICreateGroupService createGroupService,
                                 IMapper mapper,
                                 ISaveContextCommand saveContextCommand,
                                 IFindByIdQuery<User> findUserQuery)
        : base(mapper)
    {
        this.createGroupService = createGroupService;
        this.saveContextCommand = saveContextCommand;
        this.findUserQuery = findUserQuery;
    }

    [HttpPost]
    public async ValueTask<ApiResponse<GroupDto>> Create(CreateGroupRequest request)
    {
        var creator = await findUserQuery.FindRequiredAsync(request.CreatorId);

        var group = await createGroupService.CreateAsync(creator, request.Name);
        await saveContextCommand.CommitChangesAsync();

        var groupDto = mapper.Map<GroupDto>(group);
        return new CreatedResponse<GroupDto>(groupDto);
    }
}