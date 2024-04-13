using Froggie.Domain.Tasks;

namespace Froggie.Data.Tasks;

internal sealed class DeleteTaskCommand(FroggieDb froggieDb) : IDeleteTaskCommand
{
    public async ValueTask DeleteAsync(Id<Task> id)
    {
        var task = await froggieDb.Tasks.FindAsync(id);

        if(task is null)
        {
            throw new MissingEntityException(id, typeof(Task));
        }

        froggieDb.Remove(task);
    }
}