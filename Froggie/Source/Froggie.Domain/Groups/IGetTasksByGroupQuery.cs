using LittleByte.Common.Infra.Models;

namespace Froggie.Domain.Groups;

public interface IGetTasksByGroupQuery
{
    ValueTask<PageResponse<Task>> QueryAsync(Id<Group> groupId);
}