using Froggie.Domain.Users;
using LittleByte.Common.Infra.Models;

namespace Froggie.Domain.Groups;

public interface IGetUsersInGroupQuery
{
    public ValueTask<PageResponse<User>> QueryAsync(Id<Group> groupId);
}