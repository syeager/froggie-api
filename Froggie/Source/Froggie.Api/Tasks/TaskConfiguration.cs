using Froggie.Api.Tasks.Controllers;
using Froggie.Domain.Tasks.Services;

namespace Froggie.Api.Tasks;

internal static class TaskConfiguration
{
    public static IServiceCollection AddTasks(this IServiceCollection @this)
    {
        return @this
            .AddTransient<CreateTaskController>()
            .AddTransient<ICreateTaskService, CreateTaskService>();
    }
}