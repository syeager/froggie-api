using LittleByte.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Tasks;

public static class TaskConfiguration
{
    public static IServiceCollection AddTasksDomain(this IServiceCollection services) => services
        .AddTransient<IAddUserToTaskService, AddUserToTaskService>()
        .AddTransient<ICreateTaskService, CreateTaskService>()
        .AddTransient<IDeleteTaskService, DeleteTaskService>()
        .AddTransient<ITaskFactory, TaskFactory>()
        .AddTransient<ModelValidator<Task>, TaskValidator>();
}