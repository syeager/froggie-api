using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Tasks;

internal static class TaskConfiguration
{
    public static IServiceCollection AddTasks(this IServiceCollection @this) => @this
        .AddTransient<ICreateTaskService, CreateTaskService>()
        .AddTransient<IDeleteTaskService, DeleteTaskService>()
        .AddTransient<ITaskFactory, TaskFactory>();
}