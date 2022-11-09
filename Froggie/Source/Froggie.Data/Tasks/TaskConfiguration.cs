using Froggie.Domain.Tasks;
using LittleByte.Common.Infra.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Tasks;

internal static class TaskConfiguration
{
    internal static IServiceCollection AddTasks(this IServiceCollection @this) => @this
        .AddScoped<IAddTaskCommand, AddTaskCommand>()
        .AddScoped<IDeleteTaskCommand, DeleteTaskCommand>()
        .AddScoped<IFindByIdQuery<Task>, FindByIdQuery<Task, TaskDao, FroggieDb>>()
        .AddScoped<IGetTasksByUserQuery, GetTasksByUserQuery>()
        .AddScoped<ITaskPageQuery, TaskPageQuery>();
}