namespace Froggie.Domain.Tasks.Commands;

public interface IAddTaskCommand
{
    void Add(Valid<Task> validTask);
}