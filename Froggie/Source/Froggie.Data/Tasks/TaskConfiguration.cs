using Froggie.Domain.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Tasks;

public static class TaskConfiguration
{
    public static IServiceCollection AddTasksData(this IServiceCollection @this) => @this
        .AddScoped<IAddTaskCommand, AddTaskCommand>()
        .AddScoped<IDeleteTaskCommand, DeleteTaskCommand>()
        .AddScoped<IFindByIdQuery<Task>, FindByIdQuery<Task, TaskDao, FroggieDb>>()
        .AddScoped<IGetTasksByUserQuery, GetTasksByUserQuery>()
        .AddScoped<ITaskPageQuery, TaskPageQuery>()
        .AddTransient<IGetUsersTasksService, GetUsersTasksService>()
    ;
}