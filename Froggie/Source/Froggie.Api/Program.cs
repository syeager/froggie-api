using Froggie.Admin.Accounts;
using Froggie.Api;
using Froggie.Api.Accounts;
using Froggie.Api.Groups;
using Froggie.Api.Tasks;
using Froggie.Api.Users;
using Froggie.Data;
using LittleByte.AutoMapper;
using LittleByte.Serilog;
using LittleByte.Serilog.AspNet;
using Serilog;

SerilogConfiguration.CreateBootstrap();

try
{
    var builder = WebApplication
        .CreateBuilder(args)
        .UseSerilog();

    builder.Services.AddControllers()
        .ThrowValidationExceptions();

    builder.Services
        .AddOpenApi("Froggie")
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddEndpointsApiExplorer()
        .AddSingleton<StringValueObjectConverter>()
        .AddAccounts(builder.Configuration)
        .AddUsers()
        .AddTasks()
        .AddGroups()
        .AddPersistence()
        .AddAdmin()
        ;

    var app = builder.Build();

    if(app.Environment.IsDevelopment())
    {
        app
            .UseDeveloperExceptionPage()
            .UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    }
    else
    {
        app.UseExceptionHandler("/Error");
    }

    app
        .UseSerilogRequestLogging()
        .UseHttpExceptions()
        // TODO: This is not throwing an exception when I use "abc" for a guid.
        .UseModelValidationExceptions()
        .SetForwardedHeaders()
        .UseHsts()
        .UseRouting()
        .UseAuthorizationAndAuthorization()
        .UseEndpoints(endpoints => endpoints.MapControllers())
        .UseOpenApi();

    await app.AddSeedDataAsync(app.Services.CreateScope().ServiceProvider);
    app.Run();
}
catch(Exception exception)
{
    Log.Fatal(exception, "An unhandled exception occurred during bootstrapping");
    throw;
}
finally
{
    Log.CloseAndFlush();
}