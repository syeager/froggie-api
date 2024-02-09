using Froggie.Domain.Users;
using LittleByte.Domain;

namespace Froggie.Data.Tasks;

internal sealed class TaskAssignee
{
    public Id<Task> TaskId { get; init; }
    public Id<User> UserId { get; init; }
    public Task? Task { get; init; }
    public DomainModel<User>? User { get; init; }
}