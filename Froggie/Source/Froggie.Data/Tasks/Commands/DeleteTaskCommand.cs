using AutoMapper;
using Froggie.Data.Tasks.Models;
using Froggie.Domain.Tasks;
using LittleByte.Common.Domain;
using LittleByte.Common.Exceptions;

namespace Froggie.Data.Tasks.Commands;

internal sealed class DeleteTaskCommand : IDeleteTaskCommand
{
    private readonly FroggieDb froggieDb;
    private readonly IMapper mapper;

    public DeleteTaskCommand(FroggieDb froggieDb, IMapper mapper)
    {
        this.froggieDb = froggieDb;
        this.mapper = mapper;
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
