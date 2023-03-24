using AutoMapper;
using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common.Domain;

namespace Froggie.Data.Tasks;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal sealed class TaskConverter : ITypeConverter<TaskDao, Task>
{
    private readonly ITaskFactory taskFactory;

    public TaskConverter(ITaskFactory taskFactory)
    {
        this.taskFactory = taskFactory;
    }

    public Task Convert(TaskDao source, Task destination, ResolutionContext context) =>
        taskFactory.Create(
            new Id<Task>(source.Id),
            source.Title,
            new Id<User>(source.CreatorId),
            source.DueDate,
            new Id<Group>(source.GroupId));
}