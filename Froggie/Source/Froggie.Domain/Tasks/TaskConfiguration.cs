﻿using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Domain.Tasks;

internal static class TaskConfiguration
{
    public static IServiceCollection AddTasks(this IServiceCollection @this)
    {
        return @this
            .AddTransient<ICreateTaskService, CreateTaskService>();
    }
}