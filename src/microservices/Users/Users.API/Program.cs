using ServiceDefaults.CORS;
using ServiceDefaults.ErrorHandling;
using ServiceDefaults.Logging;
using ServiceDefaults.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddErrorHandling();

builder.Logging.ClearProviders();
builder.Host.AddSerilogLogging();

var corsOptions = builder.Configuration
    .GetRequiredSection(CorsOptions.SectionName)
    .Get<CorsOptions>()!;

builder.AddCors(corsOptions);

builder.AddRateLimiting();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter();

app.UseAuthorization();
app.MapControllers().RequireRateLimiting("public-api");

app.Run();
