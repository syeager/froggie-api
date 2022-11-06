using AutoMapper;
using Froggie.Domain.Tasks;

namespace Froggie.Data.Tasks;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal sealed class TaskMap : ITypeConverter<TaskDao, Task>
{
    private readonly ITaskFactory taskFactory;

    public TaskMap(ITaskFactory taskFactory)
    {
        this.taskFactory = taskFactory;
    }

    public Task Convert(TaskDao source, Task destination, ResolutionContext context) =>
        taskFactory.Create(source.Id, source.Title, source.CreatorId);
}