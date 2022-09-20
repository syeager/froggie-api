using AutoMapper;
using Froggie.Data.Tasks.Models;
using Froggie.Domain.Tasks;
using LittleByte.Common.Domain;

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
        var task = await froggieDb.FindRequiredAsync<Task, TaskDao>(id);
        var taskDao = mapper.Map<TaskDao>(task);
        froggieDb.Remove(taskDao);
    }
}
