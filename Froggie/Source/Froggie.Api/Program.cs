using Froggie.Api.Tasks;
using Froggie.Data;
using LittleByte.Common;
using LittleByte.Common.AspNet.Configuration;
using LittleByte.Common.AspNet.Middleware;
using LittleByte.Common.Dates;
using LittleByte.Common.Logging.Configuration;
using Serilog;

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
    .AddTasks()
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
    .UseRouting()
    .UseAuthorization()
    .UseHttpExceptions()
    .UseModelValidationExceptions()
    .UseEndpoints(endpoints => endpoints.MapControllers())
    .UseOpenApi();

app.Run();