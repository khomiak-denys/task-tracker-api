using MediatR;
using Messaging.PipelineBehaviors;
using MigrationsExtensions;
using Serilog;
using ServiceDefaults.Authentification;
using ServiceDefaults.Authorization;
using ServiceDefaults.CORS;
using ServiceDefaults.ErrorHandling;
using ServiceDefaults.Logging;
using ServiceDefaults.OpenApi;
using ServiceDefaults.RateLimiting;
using Tasks.Application;
using Tasks.Infrastructure;
using Tasks.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

Serilog.Debugging.SelfLog.Enable(Console.Error);

builder.Logging.ClearProviders();
builder.Host.AddSerilogLogging();

builder.AddServiceDefaults();
builder.AddErrorHandling();

var corsOptions = builder.Configuration
    .GetRequiredSection(CorsOptions.SectionName)
    .Get<CorsOptions>()!;

builder.AddCors(corsOptions);

builder.AddAuthentication();
builder.AddAuthorization();

builder.AddRateLimiting();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();

var app = builder.Build();

app.UseExceptionHandler();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations<TasksDbContext>();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("public-api");

app.Run();
