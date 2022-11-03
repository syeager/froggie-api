using Froggie.Domain.Tasks;
using LittleByte.Common.Infra.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Tasks;

internal static class TaskConfiguration
{
    internal static IServiceCollection AddTasks(this IServiceCollection @this)
    {
        return @this
            .AddScoped<ITaskPageQuery, TaskPageQuery>()
            .AddScoped<IFindByIdQuery<Task>, FindByIdQuery<Task, TaskDao, FroggieDb>>()
            .AddScoped<IAddTaskCommand, AddTaskCommand>()
            .AddScoped<IDeleteTaskCommand, DeleteTaskCommand>();
    }
}