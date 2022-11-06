namespace Froggie.Domain.Tasks;

public interface ITaskFactory
{
    Task Create(Guid idValue, string titleValue, Guid userId);
}

internal sealed class TaskFactory : ITaskFactory
{
    public Task Create(Guid idValue, string titleValue, Guid userId)
    {
        var title = new Title(titleValue);
        var task = Task.Create(idValue, title, userId);

        return task;
    }
}