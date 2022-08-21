using Froggie.Data.Tasks.Commands;
using Froggie.Data.Tasks.Models;
using Froggie.Domain.Tasks.Commands;
using LittleByte.Infra.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Tasks;

internal static class TaskConfiguration
{
    internal static IServiceCollection AddTasks(this IServiceCollection @this)
    {
        return @this
            .AddScoped<IFindByIdQuery<Task>, FindByIdQuery<Task, TaskDao, FroggieDb>>()
            .AddScoped<IAddTaskCommand, AddTaskCommand>();
    }
}