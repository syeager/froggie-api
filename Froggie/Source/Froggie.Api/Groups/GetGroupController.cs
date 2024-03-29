﻿using Froggie.Domain.Groups;
using LittleByte.Domain;

namespace Froggie.Api.Groups;

public sealed class GetGroupController(IFindByIdQuery<Group> getGroup, IMapper mapper) : GroupController
{
    [HttpGet(Routes.GetById)]
    [ResponseType(HttpStatusCode.OK, typeof(GroupDto))]
    [ResponseType(HttpStatusCode.NotFound)]
    public async ValueTask<ApiResponse<GroupDto>> GetGroup(Guid id)
    {
        var group = await getGroup.FindRequiredAsync(id.ToId<Group>());
        var dto = mapper.Map<GroupDto>(group);
        return new OkResponse<GroupDto>(dto);
    }
}