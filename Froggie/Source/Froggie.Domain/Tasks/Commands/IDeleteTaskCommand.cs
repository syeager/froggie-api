namespace Froggie.Domain.Tasks;

public interface IDeleteTaskCommand
{
    ValueTask DeleteAsync(Id<Task> id);
}