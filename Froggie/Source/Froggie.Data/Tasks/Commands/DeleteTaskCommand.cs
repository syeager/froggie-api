using Froggie.Domain.Tasks;
using LittleByte.Common.Domain;
using LittleByte.Common.Exceptions;

namespace Froggie.Data.Tasks;

internal sealed class DeleteTaskCommand : IDeleteTaskCommand
{
    private readonly FroggieDb froggieDb;

    public DeleteTaskCommand(FroggieDb froggieDb)
    {
        this.froggieDb = froggieDb;
    }

    public async ValueTask DeleteAsync(Id<Task> id)
    {
        var task = await froggieDb.Tasks.FindAsync(id.Value);
       
        if (task is null)
        {
            throw new NotFoundException(typeof(Task), id);
        }

        froggieDb.Remove(task);
    }
}
