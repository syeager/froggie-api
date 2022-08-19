using Froggie.Data.Tasks.Commands;
using Froggie.Domain.Tasks.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Data.Tasks;

internal static class TaskConfiguration
{
    internal static IServiceCollection AddTasks(this IServiceCollection @this)
    {
        return @this
            .AddScoped<IAddTaskCommand, AddTaskCommand>();
    }
}