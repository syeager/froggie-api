using Froggie.Data;
using Froggie.Domain;
using LittleByte.Common;
using LittleByte.Common.AspNet.Configuration;
using LittleByte.Common.AspNet.Middleware;
using LittleByte.Common.Identity.Configuration;
using LittleByte.Common.Logging.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{

    var builder = WebApplication.CreateBuilder(args);
    builder.UseSerilog();

    builder.Services.AddControllers();

    builder.Services
        .AddLogs()
        .AddOpenApi("Froggie")
        .AddSingleton<IDateService, DateService>()
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddDomain()
        .AddPersistence()
        .AddJwtAuthentication(builder.Configuration);

    var app = builder.Build();
    app.UseSerilogRequestLogging();

    var forwardOptions = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
        RequireHeaderSymmetry = false
    };

    forwardOptions.KnownNetworks.Clear();
    forwardOptions.KnownProxies.Clear();

    app.UseForwardedHeaders(forwardOptions);

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
        .UseHsts()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseHttpExceptions()
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
