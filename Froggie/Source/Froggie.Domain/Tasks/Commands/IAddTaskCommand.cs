namespace Froggie.Domain.Tasks;

public interface IAddTaskCommand
{
    void Add(Task validTask);
}