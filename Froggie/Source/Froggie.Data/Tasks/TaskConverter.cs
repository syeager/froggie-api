using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;

namespace Froggie.Data.Tasks;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal sealed class TaskConverter(ITaskFactory factory) : ITypeConverter<TaskDao, Task>
{
    public Task Convert(TaskDao source, Task destination, ResolutionContext context) =>
        factory.Create(
            new Id<Task>(source.Id),
            source.Title,
            new Id<User>(source.CreatorId),
            source.DueDate,
            new Id<Group>(source.GroupId));
}