using Froggie.Data;
using LittleByte.Core.Dates;
using LittleByte.Extensions.AspNet.Configuration;
using LittleByte.Extensions.AspNet.Middleware;
using LittleByte.Identity.Configuration;
using LittleByte.Logging.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.UseSerilog();

builder.Services.AddControllers();

builder.Services
    .AddLogs()
    .AddOpenApi("Froggie")
    .AddSingleton<IDateService, DateService>()
    .AddJwtAuthentication(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddPersistence();

var app = builder.Build();
app.UseSerilogRequestLogging();

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
    .UseHttpsRedirection()
    .UseHsts()
    .UseAuthentication()
    .UseRouting()
    .UseAuthorization()
    .UseHttpExceptions()
    .UseModelValidationExceptions()
    .UseEndpoints(endpoints => endpoints.MapControllers())
    .UseOpenApi();

app.Run();