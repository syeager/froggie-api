using Froggie.Data.Tasks;
using Froggie.Domain.Tasks;

namespace Froggie.Api.Tasks;

public static class TasksConfiguration
{
    public static IServiceCollection AddTasks(this IServiceCollection services) =>
        services
            .AddTasksDomain()
            .AddTasksData()
        ;
}