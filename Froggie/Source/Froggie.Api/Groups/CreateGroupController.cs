using AutoMapper;
using Froggie.Domain.Groups;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Groups;

public sealed class CreateGroupController : GroupController
{
    private readonly ICreateGroupService createGroupService;
    private readonly IMapper mapper;
    private readonly ISaveContextCommand saveContextCommand;

    public CreateGroupController(ICreateGroupService createGroupService, IMapper mapper, ISaveContextCommand saveContextCommand)
    {
        this.createGroupService = createGroupService;
        this.mapper = mapper;
        this.saveContextCommand = saveContextCommand;
    }

    [HttpPost]
    public async ValueTask<ApiResponse<GroupDto>> Create(CreateGroupRequest request)
    {
        var group = await createGroupService.CreateAsync(request.Name);
        var groupDto = mapper.Map<GroupDto>(group);
        return new CreatedResponse<GroupDto>(groupDto);
    }
}