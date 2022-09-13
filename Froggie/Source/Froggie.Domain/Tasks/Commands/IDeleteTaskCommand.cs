namespace Froggie.Domain.Tasks.Commands;

public interface IDeleteTaskCommand
{
    void Delete(Id<Task> id);
}
