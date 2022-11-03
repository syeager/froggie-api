namespace Froggie.Domain.Tasks;

public interface ITaskFactory
{
    Task Create(Guid idValue, string titleValue);
}

internal sealed class TaskFactory : ITaskFactory
{
    public Task Create(Guid idValue, string titleValue)
    {
        var title = new Title(titleValue);
        var task = Task.Create(idValue, title);

        return task;
    }
}