using Froggie.Domain.Users;
using LittleByte.Common.Infra.Models;

namespace Froggie.Domain.Tasks;

public interface IGetTasksByUserQuery
{
    ValueTask<PageResponse<Task>> RunAsync(Id<User> userId);
}