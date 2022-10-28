namespace Froggie.Domain.Tasks.Commands;

public interface IAddTaskCommand
{
    void Add(Task validTask);
}