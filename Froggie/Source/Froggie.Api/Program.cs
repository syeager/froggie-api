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
        .AddUsers(builder.Configuration)
        .AddTasks()
        .AddGroups()
        .AddPersistence()
        .AddJwtAuthentication(builder.Configuration);

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
        .SetForwardedHeaders()
        .UseHsts()
        .UseRouting()
        .UseHttpExceptions()
        .UseAuthentication()
        .UseAuthorization()
        .UseModelValidationExceptions()
        .UseEndpoints(endpoints => endpoints.MapControllers())
        .UseOpenApi();

    //await app.AddSeedDataAsync(app.Services.CreateScope().ServiceProvider);
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