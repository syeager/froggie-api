using LittleByte.Validation;

namespace Froggie.Domain.Tasks.Services;

public interface ICreateTaskService
{
    Task<Valid<Task>> CreateAsync(string title);
}

internal sealed class CreateTaskService : ICreateTaskService
{
    public Task<Valid<Task>> CreateAsync(string title)
    {
        throw new NotImplementedException();
    }
}