using Froggie.Domain.Tasks;
using LittleByte.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Tasks;

public static class TaskConfiguration
{
    public static IServiceCollection AddTasksData(this IServiceCollection @this) => @this
        .AddScoped<IAddTaskCommand, AddTaskCommand>()
        .AddScoped<IDeleteTaskCommand, DeleteTaskCommand>()
        .AddScoped<IFindByIdQuery<Task>, FindByIdQuery<Task, FroggieDb>>()
        .AddScoped<IGetTasksByUserQuery, GetTasksByUserQuery>()
        .AddScoped<ITaskPageQuery, TaskPageQuery>()
        .AddTransient<IGetUserTasksQuery, GetUserTasksQuery>()
    ;
}