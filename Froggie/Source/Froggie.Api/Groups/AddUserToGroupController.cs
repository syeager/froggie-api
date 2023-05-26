using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common.Infra.Commands;

namespace Froggie.Api.Groups;

public sealed class AddUserToGroupController : GroupController
{
    private readonly IFindByIdQuery<User> findUserQuery;
    private readonly IFindByIdQuery<Group> findGroupQuery;
    private readonly IAddUserToGroupService addUserToGroupService;
    private readonly ISaveContextCommand saveCommand;

    public AddUserToGroupController(IMapper mapper,
                                    IFindByIdQuery<User> findUserQuery,
                                    IFindByIdQuery<Group> findGroupQuery,
                                    IAddUserToGroupService addUserToGroupService,
                                    ISaveContextCommand saveCommand)
        : base(mapper)
    {
        this.findUserQuery = findUserQuery;
        this.findGroupQuery = findGroupQuery;
        this.addUserToGroupService = addUserToGroupService;
        this.saveCommand = saveCommand;
    }

    [HttpPost("add-member")]
    [ResponseType(HttpStatusCode.OK)]
    public async ValueTask<ApiResponse> AddUser(AddUserToGroupRequest request)
    {
        var user = await findUserQuery.FindRequiredAsync(request.UserId);
        var group = await findGroupQuery.FindRequiredAsync(request.GroupId);

        await addUserToGroupService.AddAsync(user, group);

        await saveCommand.CommitChangesAsync();

        return new OkResponse();
    }
}