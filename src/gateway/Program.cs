using Serilog;
using ServiceDefaults.Authentification;
using ServiceDefaults.CORS;
using ServiceDefaults.ErrorHandling;
using ServiceDefaults.Logging;
using ServiceDefaults.RateLimiting;

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
builder.Services.AddAuthorization();

builder.AddRateLimiting();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetRequiredSection("ReverseProxy"));

var app = builder.Build();

app.UseExceptionHandler();
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors(corsOptions.Name);

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("public-api");

app.Run();
